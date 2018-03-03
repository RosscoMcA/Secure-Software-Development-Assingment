package com.example.ross.mdpcoursework.DataHandling;

/**
 * Created by Ross McArthur
 * Matriculation Number: S1429389
 */

/*** Class handles all pulls to the RSS source for all types of pulls
 *
 */
public class RSSPuller {

    //Attributes process the
    private String url_accidents = "https://trafficscotland.org/rss/feeds/currentincidents.aspx";
    private String url_roadworks = "https://trafficscotland.org/rss/feeds/roadworks.aspx";
    private String url_planned_roadworks = "https://trafficscotland.org/rss/feeds/plannedroadworks.aspx";

    public RSSPuller(){

    }

    public String getAccidentsURL(){
        return url_accidents;
    }

    public String getUrl_planned_roadworks() {
        return url_planned_roadworks;
    }

    public String getUrl_roadworks(){
        return url_roadworks;
    }
}




