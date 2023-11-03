var basedURL = window.location.origin;
var rowSL = 0;


$(function () {
    $("#txtProductId").select2();
});


$("#txtProductId").change(function () {

    $("#txtProductCode").val("");
    $("#txtProductModel").val("");
    $("#txtProductBrand").val("");
    $("#txtProductUnit").val("");
    $("#txtProductPrice").val("");
    $("#txtProductStock").val("");
    $("#txtProductSell").val("");
 

    var productIdPram = parseInt($.trim(this.value), 10);

    if (productIdPram > 0) {

        $.ajax({
            url: basedURL + "/Sales/getProductRelatedData?id=" + productIdPram,
            type: 'GET',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                if (r.data) {

                    $("#txtProductCode").val(r.data.result.code);
                    $("#txtProductModel").val(r.data.result.model);
                    $("#txtProductBrand").val(r.data.result.brand);
                    $("#txtProductUnit").val(r.data.result.unit);
                    $("#txtProductPrice").val(r.data.result.price.toFixed(2));
                    $("#txtProductStock").val(r.data.result.stock);

                }
                else {
                    swal("No data found!", r.d.result, "success");
                    $(".dummy_cleare_field").val("");
                }
            },
            error: function (er) {
                $.notify(er.statusText, "error");
            }
        });
    } else {
        $(".dummy_cleare_field").val("");
    }
});




$(function () {

    $("#btnAddProductData").click(function () {

        var isValid = true;

        let itemProductId = $.trim($("#txtProductId").val());
        let itemProductName = $.trim($("#txtProductId option:selected").text());

        let itemProductCode = $.trim($("#txtProductCode").val());
        let itemProductBrand = $.trim($("#txtProductBrand").val());
        let itemProductModel = $.trim($("#txtProductModel").val());
        let itemProductUnit = $.trim($("#txtProductUnit").val());
        let itemProductPrice = $.trim($("#txtProductPrice").val());
        let itemProductStock = $.trim($("#txtProductStock").val());
        let itemProductSell = $.trim($("#txtProductSell").val());


        if (itemProductId == "" || itemProductId == 0)  {
            isValid = false;
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Please select the Product First'
            })
            return;
        }

        if (itemProductStock != "" && itemProductSell != "") {
            var StockQuantity = parseInt(itemProductStock);
            var SellingQuantity = parseInt(itemProductSell);

            if (SellingQuantity > StockQuantity) {
                isValid = false;
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Stock Limit Exists!'
                })
                return;
            }

            if (SellingQuantity <= 0) {
                isValid = false;
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Invalid Selling Input'
                })
                return;
            }
        }
        else {
            isValid = false;
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Product Stock or Selling Quantity Invalid'
            })
            return;
        }



        $('#tblLineItemList tbody tr').each(function () {
            var tblProductName = $.trim($(this).find('td:eq(2)').text());
            if (tblProductName == itemProductName) {
                isValid = false;
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Item Already Added!'
                })
            }
            return;
        });


        var totalPrice = parseFloat(itemProductPrice * itemProductSell).toFixed(2);

        if (isValid) {
            let actionLink = "<div class='btn-group' role='group'><button type='button' id='editbtn'  class='btn btn-info' style='' onclick='EditThisProductItem(this)'><i class='fa fa-edit'></i></button>";
            actionLink = actionLink + "<button type='button' class='btn btn-warning' style='margin-left: 5px;'  onclick='RemoveThisProductItem(this)'><i class='fa fa-trash'></i></button></div>";


            let tableRow = "<tr class='dummy_row_line_item' id='pi_" + (rowSL + 1) + "_line_item'>";
            tableRow = tableRow + "<td class='dummy_sl_no' style=''> <span>" + (rowSL + 1) + " </span></td>";
            tableRow = tableRow + "<td class='dummy_item_product'><input type='hidden' name='SaleDetailVm[" + (rowSL) + "].ProductId' value='" + itemProductId + "'><span>" + itemProductId + "</span></td>";
            tableRow = tableRow + "<td class='dummy_item_code'><input type='hidden' name='SaleDetailVm[" + (rowSL) + "].ProductName' value='" + itemProductName + "'> <span>" + itemProductName + "</span></td>";
            tableRow = tableRow + "<td class='dummy_item_price'><input type='hidden' name='SaleDetailVm[" + (rowSL) + "].ProductCode' value='" + itemProductCode + "'> <span>" + itemProductCode + "</span></td>";
            tableRow = tableRow + "<td class='dummy_item_stock'><input type='hidden' name='SaleDetailVm[" + (rowSL) + "].ProductBrand' value='" + itemProductBrand + "'> <span>" + itemProductBrand + "</span></td>";
            tableRow = tableRow + "<td class='dummy_item_stock'><input type='hidden' name='SaleDetailVm[" + (rowSL) + "].ProductModel' value='" + itemProductModel + "'> <span>" + itemProductModel + "</span></td>";
            tableRow = tableRow + "<td class='dummy_item_stock'><input type='hidden' name='SaleDetailVm[" + (rowSL) + "].ProductUnit' value='" + itemProductUnit + "'> <span>" + itemProductUnit + "</span></td>";
            tableRow = tableRow + "<td class='dummy_item_stock'><input type='hidden' name='SaleDetailVm[" + (rowSL) + "].UnitPrice' value='" + itemProductPrice + "'> <span>" + itemProductPrice + "</span></td>";
            tableRow = tableRow + "<td class='dummy_item_send'><input type='hidden' name='SaleDetailVm[" + (rowSL) + "].Quantity' value='" + itemProductSell + "'> <span>" + itemProductSell + "</span></td>";
            tableRow = tableRow + "<td class='dummy_item_total'><input type='hidden' name='SaleDetailVm[" + (rowSL) + "].TotalPrice' value='" + totalPrice + "' > <span class='eachTotal'>" + totalPrice + " </span></td>";
            tableRow = tableRow + "<td class='dummy_model_link'>" + actionLink + "</td>";
            tableRow = tableRow + "</tr>";
            $("#tblLineItemList").append(tableRow);

            rowSL++;

            var valueSubTotal = getSubTotalAmount();
            $("#subTotal").val(valueSubTotal);

            $(".dummy_reset_item_form").val("");

        }

    });




    $("#btnUpdateProductData").click(function () {
        debugger;
        $("#txtProductId").attr("disabled", false);
        var isValid = true;
        var rowId = $(this).attr("row");

        let itemProductId = $.trim($("#txtProductId").val());
        let itemProduct = $.trim($("#txtProductId option:selected").text());
        let itemProductPrice = $.trim($("#txtProductPrice").val());
        let itemProductCode = $.trim($("#txtProductCode").val());
        let itemStock = $.trim($("#txtStock").val());
        let itemSend = $.trim($("#txtSend").val());

        $(".clsemp").hide();
        $(".error3").hide();
        if (itemProductId == "" || itemProductCode == "" || itemStock == "" || itemSend == "") {
            isValid = false;
            if (itemProductId == "") {
                $(".clsProduct").show();
            }

            if (itemProductCode == "") {
                $(".clsCode").show();
            }

            if (itemStock == "") {
                $(".clsStock").show();
            }

            if (itemSend == "") {
                $(".clsSend").show();
            }


        }
        if (itemStock != "" && itemSend != "") {
            var StockQuantity = parseInt($.trim($("#txtStock").val()));
            var Quantity = parseInt($.trim($("#txtSend").val()));

            if (Quantity > StockQuantity) {
                isValid = false;
                $(".error2").show();
            }
        }

        //$('#tblLineItemList tbody tr').each(function () {
        //    var tblProductCode = $.trim($(this).find('td:eq(2)').text());
        //    if (tblProductCode == itemProductCode) {
        //        isValid = false;
        //        $(".error3").show();
        //    }
        //});



        if (isValid) {

            $(rowId).find(".dummy_item_product").find("input").val(itemProductId);
            $(rowId).find(".dummy_item_product").find("span").text(itemProduct);

            $(rowId).find(".dummy_item_code").find("input").val(itemProductCode);
            $(rowId).find(".dummy_item_code").find("span").text(itemProductCode);

            $(rowId).find(".dummy_item_stock").find("input").val(itemStock);
            $(rowId).find(".dummy_item_stock").find("span").text(itemStock);

            $(rowId).find(".dummy_item_send").find("input").val(itemSend);
            $(rowId).find(".dummy_item_send").find("span").text(itemSend);

            $(rowId).find(".dummy_item_price").find("input").val(itemProductPrice);
            $(rowId).find(".dummy_item_price").find("span").text(itemProductPrice);

            $(rowId).find(".dummy_item_total").find("span").text(itemProductPrice * itemSend);


            $(".dummy_reset_item_form").val("");

            $("#btnUpdateProductData").hide();
            $("#btnSaveProductData").show();
        }

        $("#SumTotal").text(getSummation());
        $("#hdnTotalSum").val(getSummation());
    })


});






function EditThisProductItem(elem) {
    $(".clsemp").hide();
    $("#txtProductId").attr("disabled", true);
    let currentRow = $(elem).closest("td").parent("tr").attr("id");
    currentRow = currentRow != "" ? $.trim("#" + currentRow) : "";

    if (currentRow == "")
        return false;

    var product = $.trim($(currentRow + " .dummy_item_product").children('input').val());

    $("#txtProductId").val(product);

    $("#txtProductPrice").val($.trim($(currentRow + " .dummy_item_price").text()));
    $("#txtProductCode").val($.trim($(currentRow + " .dummy_item_code").text()));
    $("#txtStock").val($.trim($(currentRow + " .dummy_item_stock").text()));
    $("#txtSend").val($.trim($(currentRow + " .dummy_item_send").text()));


    $("#btnUpdateProductData").attr("row", currentRow);
    $("#btnSaveProductData").hide();
    $("#btnUpdateProductData").show();
}

function RemoveThisProductItem(selector) {

    $(selector).closest("td").parent("tr").remove();

    rowSL = 0;
    $(".dummy_reset_item_form").val("");

    $(".dummy_row_line_item").each(function (index, value) {

        var thisRow = $(this);
        $(thisRow).find("td").each(function (iteration, tdItem) {

            let lineItemRow = $(this).find("input[type='hidden']")

            if (lineItemRow.length > 0) {

                var nameSuffix = lineItemRow.attr('name').match(/\d+/);

                let selectedElem = $(this).find("input[type='hidden']");
                var oldItemName = $(selectedElem).attr('name');
                var newItemName = oldItemName.replace('[' + nameSuffix + ']', '[' + (index) + ']');
                $(selectedElem).attr('name', newItemName);
            }
        });
        let slSelector = thisRow.find("td.dummy_sl_no").find("span");
        slSelector.text(rowSL + 1);
        rowSL++;


    });

    $("#SumTotal").text(getSummation());
    $("#hdnTotalSum").val(getSummation());
}

function getSubTotalAmount() {
    let sum = 0;
    $(".eachTotal").each(function () {
        let value = $(this).text();
        var number = Number(value);
        sum += number;
    });
    return sum.toFixed(2);
}