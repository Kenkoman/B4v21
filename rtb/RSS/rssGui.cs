// Create a TCPObject to be used for connecting to the server
%obj = new TCPObject(TCPObj);

//-------------------------
// Function to connect and 
// grab the RSS file
//-------------------------
function getrss()
{
	// Connect to the RSS site
	TCPObj.connect($rss::site);
}


//-------------------------
// When we're connected send
// a GET request with the URL
// of the RSS we want
//-------------------------
function TCPObj::onConnected(%this)
{
	// Output red text to show we connected
	error("Connected: http://" SPC $rss::site);
	// Send the request for the RSS file from the RSS URL
	%this.send("GET " @ $rss::folder @ "rssnews.php?url=" @ $rss::url @ " HTTP/1.0\nHost: " @ $rss::site @ "\nUser-Agent: Torque/1.0 \n\r\n");
}

//-------------------------
// Parse the return from the 
// server. 
//-------------------------
function TCPObj::onLine(%this, %line)
{
	// Check to see if return contains RSS data
	if(%line $= "[RSS START]")
	{	
		// Set our flag to show we're getting RSS data
		TCPObj.rsson = true;
		// Output Red text to show we have started getting RSS Data
		error(%line);
	}
	else if(%line $= "[RSS END]") // Check to see if we're at the end of the RSS data
	{
		// Set our flag to show we're finished getting the RSS data
		TCPObj.rsson = false;
		// Output Red text to show we have finished getting the data
		error(%line);
		// Output Red text to show we're done and disconnecting
		error("Disconnect");
		// Disconnect from the server
		%this.disconnect();
	}
	else 
	{
		// If we're in the RSS date, add the data to the MLText control
		if (TCPObj.rsson) {
			lstMLRSSView.addtext(%line @ "\n",true); // Use the bool TRUE to cause the text to reformat after each line being added.
		}
	}
	
}

//-------------------------
// View the selected RSS feed
//-------------------------
function lstRSSFeeds::getRSS( %this )
{
	// Clear the MLText control
	lstMLRSSView.settext("");
	// Find what line we have selected
	%selId = %this.getSelectedId();
	// Set the RSS URL global variable to the RSS Feed URL for the selected line
   $rss::url = $RSSFeed::url[%selId];
   // Call the function to download the RSS file
	getrss();
}

//-------------------------
// Add a new RSS feed to the
// RSS list and save it to file
//-------------------------
function lstRSSFeeds::addfeed( %this )
{
	// Increase the total number of RSS Feeds
	$RSSFeed::TotalNumber = $RSSFeed::TotalNumber + 1;
	// Add the Feed Name to the Feed Name list
  	$RSSFeed::name[$RSSFeed::TotalNumber] = txtEditFeed.getValue();
  	// Add the Feed URL to the Feed URL List
  	$RSSFeed::url[$RSSFeed::TotalNumber] = txtEditURL.getValue();
  	// Clear the Feed text edit box
  	txtEditFeed.clear();
  	// Clear the Feed URL text edit box
  	txtEditURL.clear();
  	// Hide the Add URL dialog
  	ctrlAddFeed.visible=false;
  	// Fill the RSS Feed listbox with the RSS Feed Name list
  	fillRSSList();
  	// Save the RSS Feeds to the rssfeeds.lst file
  	savefeeds();
}

//-------------------------
// Load the RSS feed list
//-------------------------
function loadfeeds() {
	// Reset total numer of RSS Feeds to 0
	$RSSFeed::TotalNumber = 0;
	// Create a new file object with a name for ease in calling
   new fileobject("rssfeeds");
   // open the rssfeeds.lst file
   if(rssfeeds.openForRead("rtb/RSS/rssfeeds.lst"))
   {
   	//read each line of the file until we reach the EOF
   	while(!rssfeeds.isEOF())
      {
      	// Increase the total number of RSS feeds
			$RSSFeed::TotalNumber = $RSSFeed::TotalNumber + 1;
			// Read in a line
      	%input = rssfeeds.readLine();
      	// Store that line into the RSS Feed Name list
      	$RSSFeed::name[$RSSFeed::TotalNumber] = %input;
      	// Read in a secon line
      	%input = rssfeeds.readLine();      	
      	// Store that line in the RSS Feed URL list
      	$RSSFeed::url[$RSSFeed::TotalNumber] = %input;
      }
   }
   // Close the fileobject
   rssfeeds.close();
   // Delete the fileobject
   rssfeeds.delete();
    
   // Fill the RSS Feed listbox with the RSS Feed Name list
	fillRSSList();
}

//-------------------------
// Save the RSS feed list
//-------------------------
function savefeeds() {
	//If we don't have any RSS Feeds.. don't do anything
	if ($RSSFeed::TotalNumber == 0)
   	return;
	// Create a new file object with a name for ease in calling   	
   new fileobject("rssfeeds");
   // open the rssfeeds.list file for output
   rssfeeds.openForWrite("RSS/rssfeeds.lst");
   // loop through the total number of RSS Feeds
   for ( %i = 1; %i != $RSSFeed::TotalNumber+1; %i++ )
   {
   	// Write the RSS Feed Name on one line
		rssfeeds.writeLine($RSSFeed::name[%i]);
		// Write the RSS Feed URL on a second line
 		rssfeeds.writeLine($RSSFeed::url[%i]);      	
   }
   // Close the fileobject
   rssfeeds.close();
   // Delete the fileobject
   rssfeeds.delete();
}

//-------------------------
// Put the RSS feeds into the
// RSS listbox.
//-------------------------
function fillRSSList()
{
	//clear the RSS Feed listbox
   lstRSSFeeds.clear();
   // If we don't have any RSS Feeds, don't do anything
   if ($RSSFeed::TotalNumber == 0)
   	return;
   // loop through the total number of RSS Feeds
	for ( %i = 1; %i != $RSSFeed::TotalNumber+1; %i++ )
   {
   	//Add the Feed Name to the Feed listbox
      lstRSSFeeds.addRow(%i,$RSSFeed::name[%i] );
	}
}
