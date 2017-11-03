$(document).ready(function(){
    getDVDinfo();

    
    $("#addDVDButton").click(function(event)
    {
        AddDvdMenu();
    });

    $("#searchButton").click(function(event)
    {
        $.ajax(
            {
                type: "GET",
                url: 'http://localhost:65065/search/'+$("#catecorySelect").val()+"/"+ $("#searchTerm").val(),
                success: function(data, status)
                {
                    var resultCount=0;
                    $.each(data, function(index, dvd)
                {
                    var SearchResults=$("#SearchResults");
                    var searchCat=$("#catecorySelect").val();
                    var searchedTerm=$("#searchTerm").val();
                    var title=dvd.title;
                    var director=dvd.director;
                    var releaseYear=dvd.realeaseYear;
                    var rating=dvd.ratingName;
                    var ID =dvd.dvdid;
                    var notes=dvd.notes;
                    var row= "<div style='border:solid 1px; color:black'>"
                    row+='<H2>Title:  '+title+'</H2>';
                    row+='<h3> Release year:  '+releaseYear+'</h3>';
                    row+='<h3> Directed by: '+director+'</h3>';
                    row+='<h3>Rated:   '+rating+'</h3>';
                    row+='<p style= "font-size:16pt"">Notes:   '+notes+'</h3>';
                    row+='</div>';
                    resultCount+=1;
                    SearchResults.append(row);
                });
                if(resultCount==0){
                       
                       var errorRow='No DVDs found that match your criteria of '+$("#catecorySelect").val()+"= "+$("#searchTerm").val();
                        
                        SearchResults.append(errorRow);
                    }
                $("#indexContainer").hide();
                $("#SearchResultsContainer").show();
                },
                error: function(){
                    
                    var errorRow='No DVDs found that match your criteria of '+$("#catecorySelect").val()+"= "+$("#searchTerm").val();
                    
                    SearchResults.append(errorRow);
                }
                
            });
    });

    $("#exitSearchButton").click(function(event)
    {
        $("#catecorySelect").val("");
        $("#searchTerm").val("");
        $("#indexContainer").show();
        $("#SearchResultsContainer").hide();
        $("#SearchResults").empty();
    });

    $("#confirmAddButton").click(function(event){
        $.ajax({
            type: 'POST',
            url: 'http://localhost:65065/dvd/',
            data: JSON.stringify({
                title: $('#addTitle').val(),
                realeaseYear: $('#addreleaseYear').val(),
                director: $('#addDirector').val(),
                RatingID: $('#addRating').val(),
                notes: $('#addNotes').val()
            }),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            'dataType': 'json',
            success: function(data,status){
                $('#addTitle').val("");
                $('addreleaseYear').val("");
                $('#addDirector').val("");
                $('#addRating').val("");
                $('#addNotes').val("");
                $('#contentRows').empty();
                $("#indexContainer").show();
                $("#addFormDiv").hide();
                getDVDinfo();
             },
             error: function(){  $("#errorMessages").append($('<li>').attr({class: 'list-group-item  list-group-item-danger'}).text("error calling web service"));
            }
             
        });
    });

$("#addCancelButton").click(function(event){
    $("#addTitle").val("");
    $("#addreleaseYear").val("");
    $("#addDirector").val("");
    $("#addRating").val("");
    $("#addNotes").val("");
    $("#indexContainer").show();
    $("#addFormDiv").hide();
});
$("#editCancelButton").click(function(event){
    $("#indexContainer").show();
    $("#editFormDiv").hide();
    $('#editTitle').val("");
    $('#editDirector').val("");
    $('#editreleaseYear').val("");
    $('#editRating').val("");
    $('#editNotes').val("");
    $('#editDVDID').val("");
});
$("#confirmEditButton").click(function(event){
    var ID=$("#editDVDID").val();
    $.ajax({
        type: "PUT",
        url: 'http://localhost:65065/dvd/'+ID,
        data: JSON.stringify({
            dvdId:ID,
            title:$("#editTitle").val(),
            realeaseYear:$("#editreleaseYear").val(),
            director:$("#editDirector").val(),
            ratingID: $("#editRating").val(),
            notes: $("#editNotes").val()
        }),
        headers:{
            'Accept': 'application/json',
            'Content-Type':'application/json'
        },
        'dataType':'json',
        success: function(){
            $('#errorMessages').empty();
            $("#contentRows").empty();
            $("#indexContainer").show();
            $("#editFormDiv").hide();
            $('#editTitle').val("");
            $('#editDirector').val("");
            $('#editreleaseYear').val("");
            $('#editRating').val("");
            $('#editNotes').val("");
            $('#editDVDID').val("");
            getDVDinfo();
        },
        error: function()
        {  
            $("#errorMessages").append($('<li>').attr({class: 'list-group-item  list-group-item-danger'}).text("Error: Please ensure all fields are filled"));
        }
    });
});

});


function getDVDinfo()
{
    var contentRows=$("#contentRows");
    $.ajax(
        {
            type: 'GET',
            url:  'http://localhost:65065/dvds/',
            success: function(data, staus)
            {
                $.each(data, function(index, dvd)
                {
                    var title=dvd.title;
                    var director=dvd.director;
                    var releaseYear=dvd.realeaseYear;
                    var rating=dvd.ratingName;
                    var ID =dvd.dvdid;
                    var row= "<tr>";
                    row+='<td style="border:solid 1px; color:black">'+title+'</td>';
                    row+='<td style="border:solid 1px; color:black">'+releaseYear+'</td>';
                    row+='<td style="border:solid 1px; color:black">'+director+'</td>';
                    row+='<td style="border:solid 1px; color:black">'+rating+'</td>';
                    row+='<td style="border:solid 1px; color:black"><a onclick="showEditForm(' + ID + ')">Edit</a></td>';
                    row+='<td style="border:solid 1px; color:black"><a onclick="deleteDVD('+ID+')">Delete</a></td>'; 
                    row+='</tr>'
                    contentRows.append(row);
                });
            },
            error: function(){
                $("#errorMessages").append($('<li>').attr({class: 'list-group-item  list-group-item-danger'}).text("error calling web service"));
            }
        });
};
function AddDvdMenu()
{
$("#indexContainer").hide();
 $("#addFormDiv").show();
};

function deleteDVD(dvdID)
{
    $("#errorMessages").empty();
    var  reallyDelete=confirm("delete this DVD?");
    if(reallyDelete){
    $.ajax(
        {
            type: 'DELETE',
            url: 'http://localhost:65065/dvd/'+dvdID,
            success: function(){
                $("#contentRows").empty();
                getDVDinfo();
            }

        });}
        else{
            return false;
        }
}

function showEditForm(dvdID)
{
    $("#errorMessages").empty();
    
    $.ajax({
        type:'GET',
        url: 'http://localhost:65065/dvd/'+dvdID,
        success: function(data, status)
        {
            $("#indexContainer").hide();
            $("#editFormDiv").show();
            $('#editTitle').val(data.title);
            $('#editDirector').val(data.director);
            $('#editreleaseYear').val(data.realeaseYear);
            $('#editRating').val(data.RatingID);
            $('#editNotes').val(data.notes);
            $('#editDVDID').val(data.dvdid);
        },
        error: function(){
            $("#errorMessages").append($('<li>').attr({class: 'list-group-item  list-group-item-danger'}).text("error calling web service"));
        }
    })
    $("#indexContainer").hide();
    $("#editFormDiv").show();
}
