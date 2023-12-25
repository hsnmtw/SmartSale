var webfetch = {
    load: function (url,success,error){
        return fetch(url,{method: 'get'}).then(success).catch(error);
    },
    get: function(url,model,success,error){
        return fetch(url,{method:'get',body:JSON.stringify(model),headers:{'content-type':'application/json'}})
        .then(success).catch(error);
    },
    post: function(url,model,success,error){
        return fetch(url,{method:'post',body:JSON.stringify(model),headers:{'content-type':'application/json'}})
        .then(success).catch(error);
    },
}