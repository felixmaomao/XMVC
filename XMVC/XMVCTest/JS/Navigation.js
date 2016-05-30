
var Nav = {

    LoadContent: function (url) {
        $.ajax({
            type: 'post',
            url: url,
            dataType: 'html',
            error: function (url) {
                alert('Error loading XML document: ' + url + "\nHttp status: " + xhr.status + " " + xhr.statusText);
            },
            success: function (data) {               
                $("#div_content").html(data);
            }
        });
    },

    SearchContent: function (url) {
        $.ajax({
            type: 'post',
            url: url,           
            data: $("#form_searchbar").serialize(),
            error: function (url) {
                alert('Error loading XML document: ' + url + "\nHttp status: " + xhr.status + " " + xhr.statusText);
            },
            success: function (data) {
                $("#div_content").html(data);
            }
        });
    },

    NavToProduct: function (url) {
        var data = this.LoadContent(url);                       
    },


    DeleteContent: function (url) {
        var row_key = $("tr.success").find("td:eq(0)").html();
        //未发现选中项的话，需要提示
        if (row_key == null) {
            fan_msg("", "提示", "请选择要删除的行", "Warn");
            return;
        }

        //需要让用户确认是否决定删除
        

        url += "?ID=";
        url += row_key;

        $.ajax({
            type: 'post',
            url: url,           
            error: function (url) {
                alert('Error loading XML document: ' + url + "\nHttp status: " + xhr.status + " " + xhr.statusText);
            },
            success: function (result) {
                eval(result);
            },
        });
    }

}