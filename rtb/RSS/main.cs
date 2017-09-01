//----------------------------
// Torque RSS Viewer
// By: Harold "LabRat" Brown
// Date: October 18, 2003
//----------------------------

//----------------------------
// CONFIGURATION
//----------------------------


$rss::site = "rtb.mocheeze.com:80"; // The URL and port to your webserver.  Do not include http:// 
$rss::folder = "/2005/"; // Path to the rssnews.php file on your webserver, include leading and trailing /'s
$rss::url = "";  // Should always be blank


//----------------------------
// Function to initialize the
// RSS Viewer
//----------------------------
	
function initRSSView() {
	// Load GUI Profile
	exec("./ui/rssguiprofiles.cs");
	
	// Load GUI
	exec("./ui/rssGui.gui");
	
	// Load core functions
	exec("./rssGui.cs");

	// Flag to denote if recieving RSS information
	TCPObj.rsson = false;
	
	//load the feeds into the list
	loadfeeds();
}

//   initRSSView();