var basedURL = window.location.origin;
var rowSL = 0;
var previousProductName = '';

$(function () {
    $("#txtProductId").select2();
    $("#btnUpdateProductData").hide();

});


$("#customerPhone").focusout(function () {

    $("#customerName").val("");
    $("#customerAddress").val("");
    $("#customerEmail").val("");

    var numberPram = $.trim(this.value);

    if (!isNaN(numberPram)) {

        $.ajax({
            url: basedURL + "/Sales/getCustomerInfoData?number=" + numberPram,
            type: 'GET',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                if (r.data) {

                    $("#customerName").val(r.data.result.name);
                    $("#customerAddress").val(r.data.result.address);
                    $("#customerEmail").val(r.data.result.email);
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


        if (itemProductId == "" || itemProductId == 0) {
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
            tableRow = tableRow + "<td class='dummy_item_pId'><input type='hidden' name='SaleDetailVm[" + (rowSL) + "].ProductId' value='" + itemProductId + "'><span>" + itemProductId + "</span></td>";
            tableRow = tableRow + "<td class='dummy_item_pName'><input type='hidden' name='SaleDetailVm[" + (rowSL) + "].ProductName' value='" + itemProductName + "'> <span>" + itemProductName + "</span></td>";
            tableRow = tableRow + "<td class='dummy_item_pCode'><input type='hidden' name='SaleDetailVm[" + (rowSL) + "].ProductCode' value='" + itemProductCode + "'> <span>" + itemProductCode + "</span></td>";
            tableRow = tableRow + "<td class='dummy_item_pBrand'><input type='hidden' name='SaleDetailVm[" + (rowSL) + "].ProductBrand' value='" + itemProductBrand + "'> <span>" + itemProductBrand + "</span></td>";
            tableRow = tableRow + "<td class='dummy_item_pModel'><input type='hidden' name='SaleDetailVm[" + (rowSL) + "].ProductModel' value='" + itemProductModel + "'> <span>" + itemProductModel + "</span></td>";
            tableRow = tableRow + "<td class='dummy_item_pUnit'><input type='hidden' name='SaleDetailVm[" + (rowSL) + "].ProductUnit' value='" + itemProductUnit + "'> <span>" + itemProductUnit + "</span></td>";
            tableRow = tableRow + "<td class='dummy_item_pPrice'><input type='hidden' name='SaleDetailVm[" + (rowSL) + "].UnitPrice' value='" + itemProductPrice + "'> <span>" + itemProductPrice + "</span></td>";
            tableRow = tableRow + "<td class='dummy_item_pStock'><input type='hidden' name='SaleDetailVm[" + (rowSL) + "].ProductStock' value='" + itemProductStock + "' ><span>" + itemProductStock + "</span></td>";
            tableRow = tableRow + "<td class='dummy_item_pQuantity'><input type='hidden' name='SaleDetailVm[" + (rowSL) + "].Quantity' value='" + itemProductSell + "'> <span>" + itemProductSell + "</span></td>";
            tableRow = tableRow + "<td class='dummy_item_pTotal'><input type='hidden' name='SaleDetailVm[" + (rowSL) + "].TotalPrice' value='" + totalPrice + "' > <span class='eachTotal'>" + totalPrice + " </span></td>";
            tableRow = tableRow + "<td class='dummy_model_link'>" + actionLink + "</td>";
            tableRow = tableRow + "</tr>";
            $("#tblLineItemList").append(tableRow);

            rowSL++;

            var valueSubTotal = getSubTotalAmount();
            $("#subTotal").val(valueSubTotal);

            $("#finalAmount").val(valueSubTotal);

            $(".dummy_reset_item_form").val("");

        }

    });




    $("#btnUpdateProductData").click(function () {
        var isValid = true;
        var rowId = $(this).attr("row");

        let itemProductId = $.trim($("#txtProductId").val());
        let itemProductName = $.trim($("#txtProductId option:selected").text());

        let itemProductCode = $.trim($("#txtProductCode").val());
        let itemProductBrand = $.trim($("#txtProductBrand").val());
        let itemProductModel = $.trim($("#txtProductModel").val());
        let itemProductUnit = $.trim($("#txtProductUnit").val());
        let itemProductPrice = $.trim($("#txtProductPrice").val());
        let itemProductStock = $.trim($("#txtProductStock").val());
        let itemProductSell = $.trim($("#txtProductSell").val());


        if (itemProductId == "" || itemProductId == 0) {
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
                if (tblProductName != previousProductName) {
                    isValid = false;
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'Item Already Added!'
                    })
                    return;
                } else {
                    return;
                }               
            }        
        });

        var totalPrice = parseFloat(itemProductPrice * itemProductSell).toFixed(2);

        if (isValid) {

            $(rowId).find(".dummy_item_pId").find("input").val(itemProductId);
            $(rowId).find(".dummy_item_pId").find("span").text(itemProductId);

            $(rowId).find(".dummy_item_pName").find("input").val(itemProductName);
            $(rowId).find(".dummy_item_pName").find("span").text(itemProductName);

            $(rowId).find(".dummy_item_pCode").find("input").val(itemProductCode);
            $(rowId).find(".dummy_item_pCode").find("span").text(itemProductCode);

            $(rowId).find(".dummy_item_pBrand").find("input").val(itemProductBrand);
            $(rowId).find(".dummy_item_pBrand").find("span").text(itemProductBrand);

            $(rowId).find(".dummy_item_pModel").find("input").val(itemProductModel);
            $(rowId).find(".dummy_item_pModel").find("span").text(itemProductModel);

            $(rowId).find(".dummy_item_pUnit").find("input").val(itemProductUnit);
            $(rowId).find(".dummy_item_pUnit").find("span").text(itemProductUnit);

            $(rowId).find(".dummy_item_pPrice").find("input").val(itemProductPrice);
            $(rowId).find(".dummy_item_pPrice").find("span").text(itemProductPrice);

            $(rowId).find(".dummy_item_pStock").find("input").val(itemProductStock);
            $(rowId).find(".dummy_item_pStock").find("span").text(itemProductStock);

            $(rowId).find(".dummy_item_pQuantity").find("input").val(itemProductSell);
            $(rowId).find(".dummy_item_pQuantity").find("span").text(itemProductSell);

            $(rowId).find(".dummy_item_pTotal").find("input").val(totalPrice);
            $(rowId).find(".dummy_item_pTotal").find("span").text(totalPrice);

            $("#btnUpdateProductData").hide();
            $("#btnAddProductData").show();
        }

        var valueSubTotal = getSubTotalAmount();
        $("#subTotal").val(valueSubTotal);

        $("#finalAmount").val(valueSubTotal);
    })
});



function EditThisProductItem(elem) {

    let currentRow = $(elem).closest("td").parent("tr").attr("id");
    currentRow = currentRow != "" ? $.trim("#" + currentRow) : "";

    if (currentRow == "")
        return false;

    var product = $.trim($(currentRow + " .dummy_item_pId").children('input').val());

    $("#txtProductId").val(product).trigger("change");

    $("#txtProductCode").val($.trim($(currentRow + " .dummy_item_pCode").text()));
    $("#txtProductBrand").val($.trim($(currentRow + " .dummy_item_pBrand").text()));
    $("#txtProductModel").val($.trim($(currentRow + " .dummy_item_pModel").text()));
    $("#txtProductUnit").val($.trim($(currentRow + " .dummy_item_pUnit").text()));
    $("#txtProductPrice").val($.trim($(currentRow + " .dummy_item_pPrice").text()));
    $("#txtProductStock").val($.trim($(currentRow + " .dummy_item_pStock").text()));
    $("#txtProductSell").val($.trim($(currentRow + " .dummy_item_pQuantity").text()));

    $("#btnUpdateProductData").attr("row", currentRow);
    $("#btnAddProductData").hide();
    $("#btnUpdateProductData").show();

    previousProductName = $.trim($(currentRow + " .dummy_item_pName").text());
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




$("#discountPercent").focusout(function () {
    parseFloat($("#lessAmount").val(0)).toFixed(2);
    parseFloat($("#paidAmount").val(0)).toFixed(2);
    parseFloat($("#dueAmount").val(0)).toFixed(2);

    const originalPrice = parseFloat($("#subTotal").val());
    const discountPercentage = parseFloat($(this).val());

    if (discountPercentage == 0) {
        $("#finalAmount").val(originalPrice);
    }

    if (discountPercentage < 0 || discountPercentage > 100) {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Invalid Discount Percent'
        })
        return;
    }
    else {

        if (!isNaN(originalPrice) && !isNaN(discountPercentage)) {
            const discountedPrice = (originalPrice - (originalPrice * discountPercentage / 100)).toFixed(2);
            $("#finalAmount").val(discountedPrice);
        }
        else {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Invalid Input'
            })
        }
    }
});


$("#lessAmount").focusout(function () {
    const originalPrice = parseFloat($("#subTotal").val());
    const discountPercentage = $("#discountPercent").val();
    const finalPrice = $("#finalAmount").val();
    const lessAmount = parseFloat($(this).val());


    if (!isNaN(lessAmount)) {
        if (discountPercentage > 0) {
            var result = parseFloat(finalPrice - lessAmount).toFixed(2);
            $("#finalAmount").val(result);
        }
        else {
            var result = parseFloat(originalPrice - lessAmount).toFixed(2);
            $("#finalAmount").val(result);
        }
    }
    else {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Invalid Input'
        })
    }
});


$("#paidAmount").focusout(function () {

    const finalPrice = parseFloat($("#finalAmount").val());
    const paidAmount = parseFloat($(this).val());


    if (!isNaN(paidAmount)) {
        if (paidAmount > 0) {
            var result = parseFloat(finalPrice - paidAmount).toFixed(2);
            $("#dueAmount").val(result);
        }
        else {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Invalid Amount'
            })
        }
    }
    else {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Invalid Amount'
        })
    }
});