package com.example.ross.mdpcoursework;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;

import com.example.ross.mdpcoursework.DataHandling.FeedData;
import com.example.ross.mdpcoursework.DataHandling.RequestHandler;

import java.util.LinkedList;

/**
 * Created by Ross McArthur
 * Matriculation Number: S1429389
 */
public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        RequestHandler rs = new RequestHandler();
        LinkedList<FeedData> feedData = rs.getRoadworks();

        if(feedData.size()==0){

        }


    }
}
