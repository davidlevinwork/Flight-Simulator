#pragma once
#include <iostream>
#include <algorithm>
#include <fstream>
#include <vector>
#include <map>
#include <sstream>
#include "pch.h"
using namespace std;

class TimeSeries {
private:
    vector<string> featuresNames;
    map<string, vector<float>> matrixMap;
    int numOfFeatures = 0;
    int numOfData = 0;
public:
    int getNumOfFeatures() const { return numOfFeatures; } /// returns number of features
    int getNumOfData() const { return numOfData; } /// returns number of lines of data in the file
    vector<string> getFeatureNames() const { return featuresNames; } /// vector that has the features names
    map<string, vector<float>> getMapMatrix() const { return matrixMap; } /// map that has all the data
    explicit TimeSeries(const char* CSVfileName); /// constructor
    virtual ~TimeSeries() {}; /// destructor (empty)
};

/// constructor - (make the matrix) save the data from the file in vectors as columns
TimeSeries::TimeSeries(const char* CSVfileName) {
    string s = CSVfileName, line;
    int location; /// will mark the location (index) of ',' in the line in the text
    ifstream inFile(s);
    if (!inFile.is_open()) { /// if there is no such file
        cout << "ERROR - can't find file" << endl;
        exit(1);
    }
    getline(inFile, line); /// line is now the first line - the features names
    numOfFeatures = count(line.begin(), line.end(), ',') + 1; /// the number of features (columns) in the file
    string firstLine = line; /// save the first line. the line of the features names
    while (getline(inFile, line)) { /// read all the text line by line
        for (int i = 0; i < numOfFeatures; i++) {
            if (numOfData == 0) { /// will happen only in the first while loop. exactly numOfFeatures times
                vector<float> forBuildingTable; /// empty vector to enter to the map with the matching key
                location = firstLine.find(',');
                string sub = firstLine.substr(0, location);
                featuresNames.push_back(sub); /// to fill the vector of the feature names
                matrixMap.insert(pair<string, vector<float>>(sub, forBuildingTable));
                firstLine = firstLine.substr(location + 1, firstLine.size() - location); /// cut the read part
            }
            location = line.find(',');
            string sub = line.substr(0, location);
            matrixMap.at(featuresNames[i]).push_back(stof(sub)); /// enter to the matching vector in map
            line = line.substr(location + 1, line.size() - location); /// cut the read part
        }
        numOfData++; /// count number of lines (without the line of features names) in the file
    }
    inFile.close();
}