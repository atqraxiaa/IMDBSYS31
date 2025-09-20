<?php
$file = "students.txt";

if (isset($_POST['clear'])) {
    file_put_contents($file, "");
    echo "<p>All student records cleared!</p>";
    echo '<a href="index.html">Go back</a>';
    exit;
}

if (isset($_POST['register'])) {
    $name = htmlspecialchars($_POST['name']);
    $id = htmlspecialchars($_POST['id']);
    $course = htmlspecialchars($_POST['course']);

    $entry = "Name: $name | ID: $id | Course: $course\n";

    file_put_contents($file, $entry, FILE_APPEND);

    echo "<p>Student registered successfully!</p>";
    echo '<a href="index.html">Register another</a>';
}
?>
