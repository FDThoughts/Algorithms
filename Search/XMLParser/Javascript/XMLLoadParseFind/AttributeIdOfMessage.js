// use DomParser to parse the XML text
function parseXML(xmlString) {
    var parser = new DOMParser();

    // Parse a simple invalid XML source
    // to get namespace of <parsererror>
    var docError = parser.parseFromString(
        'INVALID', 'text/xml'
    );
    var parsererrorNS = 
        docError.getElementsByTagName(
            "parsererror"
        )[0].namespaceURI;
    
    // Parse xmlString:
    // (XMLDocument object)
    var doc = parser.parseFromString(
        xmlString, 'text/xml'
    );
    if (doc.getElementsByTagNameNS(
        parsererrorNS, 'parsererror'
    ).length > 0) {
        throw new Error('Error parsing XML');
    }

    return doc;
}

// Get ids of entries that contains a specific message
function getIdsByMessage (
    xml, message) 
{
    ids = [];

    // Create Doc
    var doc;
    try{
        // XMLDocument object
        doc = parseXML(xml);
    }
    catch (e) {
        console.log("Error: " + e);
        return;
    }

    // Element object <--> <log>
    var rootElement = doc.documentElement;

    // Find all attributes and messages
    var children = rootElement.childNodes;    
    for (var i=0; i<children.length; i++)
    {
        var child = children[i];
        if (child.nodeType == Node.ELEMENT_NODE) {
            var id = child.getAttribute("id");
            var messageElement = 
                child.getElementsByTagName("message")
                [0];
            
            var messageText = 
                messageElement.textContent;

            if (messageText == message)
            {
                // return all ids that contains the message
                ids.push(id);
            }
        }
    }

    return ids;
}

// Main - start point
function main () {
    // XML String:
    var xmlString = 
        "<?xml version='1.0' encoding='UTF-8'?>" +
        "<log>" +
        "    <entry id='1'>" +
        "        <message>Application started</message>" +
        "    </entry>" +
        "    <entry id='2'>" +
        "        <message>Application ended</message>" +
        "    </entry>" +
        "</log>";

    // display ids
    getIdsByMessage(xmlString, "Application ended")
        .forEach(id => console.log("Entry: " +
            id + " contains message 'Application ended'"));
}

main()