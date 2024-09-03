// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//document.getElementById('searchQuery').addEventListener('input', function () {
//    let query = this.value;
//    console.log("Hello");
//    console.log("query", query);

//    fetch(`/Product/Search?key=${query}`)
//        .then(response => {
//            response.text();
//            //console.log(response);
//        })
//        .then(html => {
//            if (html) {
//                document.getElementById('searchResults').innerHTML = html;
//            } else {
//                console.log('Received empty HTML response');
//            }
//        })
//        .catch(error => {
//            console.error('Error fetching search results:', error);
//        });
//});



document.getElementById('searchQuery').addEventListener('input', function () {
    let query = this.value.toLowerCase();
    let tb = document.getElementsByClassName('table');
    let tr = document.getElementsByClassName('tr');
    let div = document.getElementById('searchResults');
    console.log("query", query);

        div.innerText="";
        let ul = document.createElement('ul');
        div.appendChild(ul);
    if (query != "" || query != null) {

        for (var i = 1; i < tr.length; i++) {
            let data = tr[i].getElementsByClassName('ProductName')[0];
            if (data != null) {
                let text = data.innerText;
                console.log("text", text)
                if (text.toLowerCase().startsWith(query)) {
                    let li = document.createElement('li');
                    let p = document.createElement('p');
                    li.appendChild(p);
                    p.innerHTML = text;
                    console.log("p", p.text);
                    console.log("text", text);
                    ul.appendChild(li);
                    console.log("div", div.innerHTML);

                }

            }


        }
    }

            
      
    

});