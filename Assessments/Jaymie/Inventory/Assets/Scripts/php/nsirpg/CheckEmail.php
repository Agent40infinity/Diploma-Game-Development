<?php
// Server Login variables
	$server_name = "localhost";
	$server_username = "root";
	$server_password = "";
	$database_name = "nsirpg";
	
	$email = $_POST["email_Post"];
	
// Check connection
	$conn = new mysqli($server_name, $server_username, $server_password, $database_name);
	if(!$conn)
	{
		die("Connection Failed. ".mysqli_connect_error());
	}	
	$checkAccount = "SELECT username FROM users WHERE email = '".$email."'";
	$checkResult = mysqli_query($conn,$checkAccount);
	
// Check for Account
	if(mysqli_num_rows($checkResult) > 0)
	{
		while($row = mysqli_fetch_assoc($checkResult))
		{
			echo $row['username'];
			return;
		}
	}
	else
	{
		echo "User Not Found";
		return;
	}
?>