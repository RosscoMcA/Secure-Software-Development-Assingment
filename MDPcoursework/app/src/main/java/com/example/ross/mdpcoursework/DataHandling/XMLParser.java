package com.example.ross.mdpcoursework.DataHandling;

import android.util.Log;

import org.xmlpull.v1.XmlPullParser;
import org.xmlpull.v1.XmlPullParserException;
import org.xmlpull.v1.XmlPullParserFactory;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.StringReader;
import java.net.URL;
import java.net.URLConnection;
import java.security.spec.ECField;
import java.util.LinkedList;
import java.util.concurrent.CountDownLatch;
import java.util.concurrent.TimeUnit;

import javax.xml.transform.Result;

/**
 * Created by Ross McArthur
 * Matriculation Number: S1429389
 */

//Class handles the parsing of data from rss feeds
public class XMLParser  {
    LinkedList<FeedData> items = new LinkedList<FeedData>();
    private boolean done = false;
    public LinkedList<FeedData> run(String feed){

        try
        {

            startTask(feed);


        }
        catch (Exception e){}

        return getData();
    }

    private LinkedList<FeedData> getData() {

        while(!done) {
            if (done) {
                return items;
            }
        }
        return  items;
    }

    private void startTask(String path) throws InterruptedException {

        new Thread(new Task(path)).start();


    }


    public class Task implements Runnable {
        private String path;


        public Task(String path) {
            this.path = path;

        }

        @Override
        public void run(){

            URL aurl;
            URLConnection yc;
            BufferedReader in = null;
            String inputLine = "";
            try {
                aurl = new URL(path);
                yc = aurl.openConnection();
                in = new BufferedReader(new InputStreamReader(yc.getInputStream()));

                XmlPullParserFactory factory = XmlPullParserFactory.newInstance();

                factory.setNamespaceAware(true);

                XmlPullParser xpp = factory.newPullParser();



                xpp.setInput(in);

                int eventType = xpp.getEventType();

                FeedData feedEntry = new FeedData();
                String field= "";
                boolean entity = false;
                while (eventType != XmlPullParser.END_DOCUMENT)
                {


                    if(eventType == XmlPullParser.START_DOCUMENT)
                    {

                        System.out.println("Start document");

                        Log.e("MyTag","Start document");

                    }
                    else
                    if(eventType == XmlPullParser.START_TAG)
                    {
                        String temp = xpp.getName();

                        if(temp.equals("item")) {

                            entity = true;
                        }

                        field = temp;
                    }
                    else
                    if(eventType == XmlPullParser.END_TAG)
                    {

                        String temp = xpp.getName();
                        if(temp.equals("item")){

                            items.add(feedEntry);

                            entity = false;

                            feedEntry = new FeedData();
                        }

                    }
                    else
                    if(eventType == XmlPullParser.TEXT)
                    {
                        String temp = xpp.getText();
                        if(!temp.contains("\n")){
                        if(entity) {
                            switch (field) {
                                case "title":
                                    feedEntry.setTitle(temp);
                                    break;
                                case "description":
                                    feedEntry.setDescription(temp);
                                    break;
                                case "georss:point":
                                    feedEntry.setPublicationDate(temp);
                                    break;
                                case "pubDate":
                                    feedEntry.setPublicationDate(temp);
                                default:
                                    break;
                            }
                        }
                        }

                        System.out.println("Text "+temp);

                        Log.e("MyTag","Text is "+temp);
                    }





                    eventType = xpp.next();

                } // End of while



                in.close();

                done = true;

            }
            catch (IOException ae)
            {
                Log.e("MyTag", "ioexception");
            } catch (XmlPullParserException e) {
                e.printStackTrace();
            }


        }



    }
}
