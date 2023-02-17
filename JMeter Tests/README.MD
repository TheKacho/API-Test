## Summary of JMeter performance with Yahoo.com

What was discovered with this performance test with Yahoo.com is that initially took all 1500 users several seconds to load
due to the amount of images and multiple front-end elements during the time of the test. What was interesting is that the longer
the test was performing, most if not all number of samples load Yahoo faster. What can be noted from this is the fact that the site 
has been temporarily cached so Yahoo and or the samples do not use any more bandwidth to constantly load during the HTTP request.

## Summary Report Chart
![summary chart](https://i.imgur.com/BnvwwU6.jpg)

## Aggregate Report
![aggregate chart](https://i.imgur.com/FLp9rnb.jpg)

## Graph Results
![graph](https://i.imgur.com/1FEkHFN.jpg)

## Backend Listener
![backend](https://i.imgur.com/7EdIfyL.jpg)