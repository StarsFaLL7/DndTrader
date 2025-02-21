var SearchDropdown = {};
SearchDropdown.InitSearchDropdown = function (pDotNetReference, guid) {
    let $eventSelect = $("#" + guid.toString());
    $eventSelect.on("select2:select", function (evt) {
        pDotNetReference.invokeMethodAsync('SelectItemInternal', evt.params.data.id);
        console.log("select2:select", evt.params.data.id);
    });
};

SearchDropdown.SelectItem = function (pDotNetReference, guid, itemValue) {
    let $eventSelect = $("#" + guid.toString());
    $eventSelect.val(itemValue); // Select the option with a value of itemValue
    $eventSelect.trigger('change');
};
