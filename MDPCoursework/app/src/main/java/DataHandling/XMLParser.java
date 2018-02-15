package DataHandling;

import android.util.Log;

import org.xmlpull.v1.XmlPullParser;
import org.xmlpull.v1.XmlPullParserException;
import org.xmlpull.v1.XmlPullParserFactory;

import java.io.IOException;
import java.io.StringReader;
import java.util.LinkedList;

/**
 * Created by Ross McArthur
 * Matriculation Number: S1429389
 */

//Class handles the parsing of data from rss feeds
public class XMLParser {


    public LinkedList<FeedData> parseData(String feed){

        try
        {
            LinkedList<FeedData> items = new LinkedList<FeedData>();

            XmlPullParserFactory factory = XmlPullParserFactory.newInstance();

            factory.setNamespaceAware(true);

            XmlPullParser xpp = factory.newPullParser();

            xpp.setInput( new StringReader( feed ) );

            int eventType = xpp.getEventType();

            FeedData feedEntry = new FeedData();
            while (eventType != XmlPullParser.END_DOCUMENT)
            {
                String field= "";

                if(eventType == XmlPullParser.START_DOCUMENT)
                {

                    System.out.println("Start document");

                    Log.e("MyTag","Start document");

                }
                else
                if(eventType == XmlPullParser.START_TAG)
                {
                    String temp = xpp.getName();

                    field = temp;
                }
                else
                if(eventType == XmlPullParser.END_TAG)
                {

                    String temp = xpp.getName();
                    if(temp.equals("item")){
                        items.add(feedEntry);

                        feedEntry = new FeedData();
                    }

                }
                else
                if(eventType == XmlPullParser.TEXT)
                {
                    String temp = xpp.getText();

                    switch(field)
                    {
                        case "title":
                            feedEntry.setTitle(temp);
                            break;
                        case "description" :
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


                    System.out.println("Text "+temp);

                    Log.e("MyTag","Text is "+temp);
                }





                eventType = xpp.next();

            } // End of while



            return items;
        }
        catch(XmlPullParserException ex ){

            Log.e("Error", "Parsing Error"+ex.toString());
        }
        catch (IOException ex){
            Log.e("Error", "IO Error during parsing");
        }

        return null;
    }
}
