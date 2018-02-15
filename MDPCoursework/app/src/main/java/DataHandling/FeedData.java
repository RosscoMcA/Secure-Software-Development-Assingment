package DataHandling;



/**
 * Created by Ross McArthur
 * Matriculation Number: S1429389
 */

//POJO: Stores all contents of a single item of  feed data
public class FeedData {

    private String Title;

    private String Description;

    private String Location;


    private String PublicationDate;


    // Getters and setters

    public String getPublicationDate() {
        return PublicationDate;
    }

    public void setPublicationDate(String publicationDate) {
        PublicationDate = publicationDate;
    }

    public String getDescription() {
        return Description;
    }

    public String getTitle() {
        return Title;
    }

    public String getLocation() {
        return Location;
    }

    public void setDescription(String description) {
        Description = description;
    }

    public void setLocation(String location) {
        Location = location;
    }

    public void setTitle(String title) {
        Title = title;
    }

    //Overwritten constructor sets parameters
    public FeedData(String title, String description, String location, String publicationDate)
    {
        Title = title;
        Description = description;
        this.Location = location;
        PublicationDate= publicationDate;
    }

    public FeedData(){

    }

}
