using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecureSoftwareApplication.Services
{
    /// <summary>
    /// Class handles the file storage service for storing files before they can be sent accross for updates to other repositories
    /// </summary>
    public class FileStorageService
    {
        private const string bucketName = "securesoftware";

        /// <summary>
        /// Adds a new file to the file storage service 
        /// </summary>
        /// <param name="file">The file to add to the store </param>
        /// <returns>The key of the item stored on the cloud</returns>
        public string Upload(HttpPostedFileBase file)
        {

            var client = new AmazonS3Client(Amazon.RegionEndpoint.EUWest1);

            try
            {
                PutObjectRequest request = new PutObjectRequest
                {
                    CannedACL = S3CannedACL.PublicRead,
                    BucketName = bucketName,
                    Key = Guid.NewGuid().ToString(),
                    InputStream = file.InputStream,
                    ContentType = "Document"
                };

                var response = client.PutObject(request);

                return request.Key;
            }
            catch (AmazonS3Exception e)
            {
                if (e.ErrorCode != null &&
                    (e.ErrorCode.Equals("InvalidAccessKeyId")
                    || e.ErrorCode.Equals("InvalidSecurity")))
                {
                    throw new Exception("Check the provided AWS Credentials");
                }
                else
                {
                    throw new Exception("Error occured: " + e.Message);
                }
            }
        }

        /// <summary>
        /// Gets the file from the bucket based on the key entered
        /// </summary>
        /// <param name="key">The File Key</param>
        /// <returns>The Stream of the file stored. </returns>
        public GetObjectResponse GetFile(string key)
        {
            using (var client = new AmazonS3Client(Amazon.RegionEndpoint.EUWest1))
            {
                GetObjectRequest request = new GetObjectRequest()
                {
                    BucketName = bucketName,
                    Key = key
                };

                var response = client.GetObject(request);


                return response;
            }
        }

    }
}