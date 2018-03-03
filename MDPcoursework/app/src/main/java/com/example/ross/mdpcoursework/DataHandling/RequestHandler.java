package com.example.ross.mdpcoursework.DataHandling;


import android.util.Log;

import java.util.LinkedList;

/*
    Created on the 2/3/2018
    Created by Ross McArthur
    Student Number:

    CLASS: Acts as an interface for aquiring specific subsets of data
 */
public class RequestHandler {

    //Gets the accident feed as a list of items
    public LinkedList<FeedData> getAccidents(){
        RSSPuller source = new RSSPuller();
        String conn = source.getAccidentsURL();

        LinkedList<FeedData> accidents =  getFeed(conn);

        return accidents;
    }

    //Gets the planned roadworks feed as a list of items
    public LinkedList<FeedData> getPlanned(){
        RSSPuller source = new RSSPuller();
        String conn = source.getUrl_planned_roadworks();


        return getFeed(conn);
    }

    //Gets the current roadworks feed as a list of items
    public LinkedList<FeedData> getRoadworks(){
        RSSPuller source = new RSSPuller();
        String conn = source.getUrl_roadworks();


        return getFeed(conn);
    }


    //Gets feed data of any type, generic pull function
    public LinkedList<FeedData> getFeed(String conn){

        XMLParser parser = new XMLParser();

        LinkedList<FeedData> feedData= parser.run(conn);


        if(feedData.size()!= 0) return feedData;
        else {

            Log.e("Error", "No Accident data found");

            return feedData;
        }

    }
}
