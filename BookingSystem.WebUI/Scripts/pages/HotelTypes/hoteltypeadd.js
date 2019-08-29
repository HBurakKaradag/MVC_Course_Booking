var HotelTypeAdd = function () {
    var jsUrlActions = {};

    var handleEvents = function () {
        $(".btnSave").click(function () {
            


            // manuel js validation
            if ($("#Title").val() == "") {
                // mesaj sonrasında return etmelisiniz.
                Core.showNotify("<b>Validation</b>", "Title field must be required", "warning");
                return;
                
            }

            var req = Core.createModel();

            // ekrandaki field'ları zaten create model ile alıyorduk, o halde req içersindeki prop üzerinden de kontrol sağlayabiliriz
            if (req.Description == "") {
                Core.showNotify("<b>Validation</b>", "Description field must be required", "warning");
                return;
            }

            // veya Core içerisine validation kontrol eden bir fucntion yazabilirsiniz.
            // örneğin required olarak işaretlenmiş bir html objesnin browser'da nasıl create edildiğini inceleyin 
            // Browser'dan Dom element'e sağ tıklayıp incele dediğinizde html 'i görüntüleyebilirsiniz.
            /*
               <input class="form-control" data-model="Title" data-type="String" data-val="true" data-val-required="Title alanı gereklidir." id="Title" name="Title" placeholder="Title" type="text" value="">
               yukarıdaki gibi bir html oluşmuş.
               data-val-required attribute'dan yakalayabilip mesaj oluşturabilirsiniz.
             */
            
            $.ajax({
                url: jsUrlActions.saveUrlAction,
                dataType: "json",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(req),
                async: true,
                processData: false,
                cache: false,
                success: function (data) {
                    /*
                     Bu kısımlarda data dan dönen obje içerisndeki verileri kullanıp daha anlamlı mesajlar veya farklı aksiyonlar alabilirsiniz.
                     */
                    
                    if (data.ResultType == Core.responseStatus.Success) {
                        Core.showNotify("<b>Complate Successfully</b>", "", "success");
                        Core.redirectPageAfterSecond(jsUrlActions.listPageUrlAction);
                    }
                    else {
                        Core.showNotify("<b>Warning..</b>", data.Message, "warning");
                        return;
                    }
                    
                 
                },
                error: function (xhr) {
                    /*
                     Bu kısımlarda xhr dan dönen obje içerisndeki verileri kullanıp daha anlamlı mesajlar veya farklı aksiyonlar alabilirsiniz.
                     */
                    Core.showNotify("<b>Get an Error</b>", "", "error");

                }
            });
        });
    }

    return {
        init: function (params) {
            jsUrlActions = params;
            handleEvents();
        }
    };
}();
