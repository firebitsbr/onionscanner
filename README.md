Onion Scanner
=============
**Created:** 2015-06-01<br>
**Author:** Darkvengance<br>
**Version:** 1.4<br>
**Updated:** 2015-06-05<br>

#Purpose
Onion Scanner is a simple TOR Domain scanner.  It has the ability to
scan .onion domains from a list (with one domain per line) or,
if no list is provided, it will scan all the links provided by
Yet Another Tor Directory (YATD).

If you wish to can visit <a href="http://bdpuqvsqmphctrcs.onion/">YATD</a>
yourself or you can use <a href="http://hss3uro2hsxfogfq.onion/">notEvil</a> which is a DarkNet search engine.
Keep in mind that if you use these they will not neccesarily give you
active, working links.

#Options

<table>
<tr><th>Short</th><th>Long</th><th>Input</th><th>Required</th><th>Description</th></tr>
<tr><td>i</td><td>input</td><td>string</td><td>No</td><td>File to read URLs from</td></tr>
<tr><td>o</td><td>output</td><td>string</td><td>Yes</td><td>Directory to write lists to</td></tr>
<tr><td>v</td><td>verbose</td><td>bool</td><td>No</td><td>Enables verbosity</td></tr>
<tr><td>u</td><td>proxyurl</td><td>string</td><td>No</td><td>The URL for the HTTP Proxy to get to TOR [use privoxy]</td></tr>
<tr><td>p</td><td>proxyport</td><td>int</td><td>No</td><td>The port number for the HTTP Proxy to get to TOR [use privoxy]</td></tr>
<tr><td>s</td><td>save</td><td>bool</td><td>No</td><td>If set saves bad and seized links as well</td></tr>
<tr><td>d</td><td>desc</td><td>bool</td><td>No</td><td>If set saves site title with URLs</td></tr>
<tr><td>a</td><td>agent</td><td>string</td><td>No</td><td>The User-Agent to emulate</td></tr>
<tr><td>n</td><td>numlinks</td><td>int</td><td>No</td><td>Number of links to scan [0=all]</td></tr>
</table>

#Setup
Since Onion Scanner currently has no method of utilizing a SOCKS5 proxy, it relys on some kind of HTTP proxy that has the ability to forward all traffic to TOR.  The best proxy I have found to do the job is <a href="http://privoxy.org/">Privoxy</a>.

In order to use privoxy with TOR you must first install Privoxy.  Next edit the config file and search for the line
>\# forward-socks5 / localhost:9050 .

Uncomment this line and change the Port Number to whatever port TOR uses, now just configure a browser to use privoxy via HTTP Proxy to see if it's working.  You can check over at <a href="https://check.torproject.org/">TORCheck</a>.

By default Onion Scanner uses port number 8118 and the url 127.0.0.1 to connect to privoxy.  You can change this using the \-u and \-p parameters respectively.

#Usage
Once you have Onion Scanner setup, you can now run it using the paramters provided in the "Options" section.  If you have a list of .onion URLs that you would like to scan, simply feed it into the program using the \-i option.  Just ensure that there is only 1 URL per line.

If you do not have a list of URLs, Onion Scanner will grab all the URLs from YetAnotherTorDirectory and check them.  It could take quite some time since (at the time of this writing) there are well over 8k URLs.