//沈伟
//根据给出的url取得json返回值，再根据传输过来的table id动态创建td 
function fan_grid(id, dataurl)
{
    //当前页面搜索框是否有内容？有的话要拼接查询字符串
    var searchData = $("#form_searchbar").serialize();
    dataurl += "?";
    dataurl += searchData;
   

    $.ajax({
        url: dataurl,
        success: function (data) {
            var result = JSON.stringify(data); //返回的json串
            var table = document.getElementById(id);
            var arr = eval(result);
            for (var i = 0; i < arr.length; i++) {                
                var jsonObj = arr[i]; //获取单个json对象                
                var tr = table.insertRow(table.rows.length);
                for(var x in jsonObj)
                {
                    var td = tr.insertCell();
                    td.align = "center";
                    td.innerHTML=jsonObj[x];
                }                
            }
        }
    })   
}


function fan_ajaxlist(id, dataurl) {
    $.ajax({
        url: dataurl,
        success: function (data) {
            var result = JSON.stringify(data);            
            var ul = document.getElementById(id);
            var arr = eval(result);            
            for (var i = 0; i < arr.length;i++)
            {
                var jsonobj = arr[i];
                var li = document.createElement("li");                
                li.innerHTML = jsonobj;
                ul.appendChild(li);
            }
        }
    })
}


//dialog对话框 沈伟
function fan_initpopup(url,popupType) {
    var popupdiv = document.createElement("div");
    var $popupdiv = $(popupdiv);

    //针对add还是edit，edit要增加参数
    if(popupType=="PopupEdit")
    {
        var row_key = $("tr.success").find("td:eq(0)").html();
        //未发现选中项的话，需要提示
        if(row_key==null)
        {
            fan_msg("", "提示", "请选择要修改的行", "Warn");
            return;
        }
        url += "?ID=";
        url += row_key;
    }

    $.ajax({
        url: url,
        success: function (data) {
            $popupdiv.html(data);
        }
    });

    $popupdiv.dialog({
        resizable: true,
        height: 500,
        width: 600,
        modal: false,
        //按钮
        buttons: {
            "取消": function () {
                //关闭按钮
                $(this).dialog("close");
            },
            "确定": function () {
                $(this).dialog("close");
                $(this).find("form").submit();               
            }           
        }
    });
}

//下拉菜单 沈伟
function fan_ajaxdropdown(id,url)
{
    $.ajax({
        url: url,
        success: function (data) {
            var result = JSON.stringify(data);
            var select = document.getElementById(id);
            var arr = eval(result);
            for (var i = 0; i < arr.length;i++)
            {
                var jsonobj = arr[i];
                var option = document.createElement("option");
                option.setAttribute("value",jsonobj.key);
                option.innerText = jsonobj.value;
                select.appendChild(option);
            }
        }
    })
}

//消息提示框 沈伟
function fan_msg(id, title, content,msgtype) {
    var popupdiv = document.createElement("div");
    var $popupdiv = $(popupdiv);
    var $icon;
    switch(msgtype)
    {
        case "Info":
            $icon = "<i class=\"icon-info-sign\"></i>"
            break;
        case "Success":
            $icon = "<i class=\"icon-ok-sign\"></i>"
            break;
        case "Warn":
            $icon = "<i class=\"icon-exclamation-sign\"></i>"
            break;
        case "Error":
            $icon = "<i class=\"icon-warning-sign\"></i>"
            break;           
    }    
    $popupdiv.html($icon+content);
    $popupdiv.dialog({
        resizable: false,
        height: 230,
        width: 360,       
        title: title,        
        modal: false,
        draggable: false,
        hide: { effect: "slideUp", duration: 800 },
        position: { my: "center", at: "top", of: window },
        //按钮
        buttons: {
            "取消": function () {
                //关闭按钮
                $(this).dialog("close");
            },
            "确定": function () {
                $(this).dialog("close");                               
            }
        }
    });
    setTimeout(function () {$popupdiv.dialog("close") },2000);

}
