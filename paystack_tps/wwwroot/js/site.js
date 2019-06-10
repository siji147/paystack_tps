// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function httpClient(url, method, data, headers) {
    var promise = new Promise(function (resolve, reject) {
        var response = {
            StatusCode: undefined,
            Data: undefined
        };
        var http = new XMLHttpRequest();
        http.open(String(method), url, true); //false makes it synchronious
        http.setRequestHeader("Content-Type", "application/json");

        if (headers) {
            headers.forEach(function (element) {
                var name = element.Name;
                var value = element.Value;
                if (name !== undefined && value !== undefined) {
                    http.setRequestHeader(name, value);
                }
            }, this);
        }

        if (data) {
            http.send(JSON.stringify(data));
        } else {
            http.send();
        }
        http.onreadystatechange = function () {
            switch (http.readyState) {
                case 4:
                    if (http.status === 200) {
                        response.StatusCode = http.status;
                        if (http.response !== "") {
                            response.Data = JSON.parse(http.response);
                        }
                        resolve(response);
                    } else {
                        reject(new Error('Something went wrong'));
                    }
                    break;
                default:
            }
        };
    });
    return promise;
}