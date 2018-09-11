$(document).ready(function(){
    //Change button color and background on mouse enter and exit
    $('.btn-primary').mouseenter(function(){
        $(this).css("background-color", "silver")
    })
    $('.btn-primary').mouseout(function(){
        $(this).css("background-color", "#494980")
    })
    //Change login glyph color on mouse enter and exit
    $('.glyph-login').mouseenter(function(){
        $(this).css("color", "#7eaeda")
    })
    $('.glyph-login').mouseout(function(){
        $(this).css("color", "white")
    })
});

//AJAX call to filter by playstation title
$(document).on('submit', '#playstationFilter', function(e){
    e.preventDefault();
    $.post("/lobbies/playstation", $('#playstationFilter').serialize(), function(response){
        for(lobby of response)
        {
            DisplayPlaystationLobby(lobby);
        }
        $('#playstationFilter')[0].reset();
    })
});

//AJAX call to filter by xbox title 
$(document).on('submit', '#xboxFilter', function(e){
    e.preventDefault();
    $.post("/lobbies/xbox", $('#xboxFilter').serialize(), function(response){
        for(lobby of response)
        {
            DisplayXboxLobby(lobby);
        }
        $('#xboxFilter')[0].reset();
    })
})

//Function to display playstation lobbies
function DisplayPlaystationLobby(lobby){
    var playstationDesc = "";
    if(lobby.description != null)
    {
        playstationDesc = lobby.description;
    }
    $('.playstation-container').replaceWith(`<div class="playstation-lobbies"></div>`)
    $('.playstation-lobbies').prepend(
        `<div class="row row-lobby">
            <div class="col-xs-4 col-scroll">
                <a href="https://my.playstation.com/profile/${lobby.gamertag}">${lobby.gamertag}</a>
                <p><span><i class="fab fa-xbox"></i></p>
            </div>
            <div class="col-xs-2">
                <p>Need</p>
                <p>${lobby.numPlayers}</p>
            </div>
            <div class="col-xs-6 col-scroll">
                <p>${lobby.gameTitle}</p>
                <p>${playstationDesc}</p>
                <p>${lobby.tags}</p>
            </div>
        </div>`
    )
}

//Function to display xbox lobbies
function DisplayXboxLobby(lobby){
    var xboxDesc = "";
    if(lobby.description != null)
    {
        xboxDesc = lobby.description;
    }
    $('.xbox-container').replaceWith(`<div class="xbox-lobbies"></div>`)
    $('.xbox-lobbies').prepend(
        `<div class="row row-lobby">
            <div class="col-xs-3 col-scroll">
                <a href="https://account.xbox.com/en-us/profile?gamertag=${lobby.gamertag}">${lobby.gamertag}</a>
                <p><span><i class="fab fa-xbox"></i></p>
            </div>
            <div class="col-xs-2">
                <p>Need</p>
                <p>${lobby.numPlayers}</p>
            </div>
            <div class="col-xs-6 col-scroll">
                <p>${lobby.gameTitle}</p>
                <p>${xboxDesc}</p>
                <p>${lobby.tags}</p>
            </div>
        </div>`
    )
}


