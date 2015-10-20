function finance() { }

finance.toastrConfirm = function (event) {
    event.preventDefault();
    var msg = '<div>Are you sure?</div><div><a id="okBtn" class="btn btn-danger">Yes</a>' +
        '<button type="button" id="btnCancel" class="btn btn-primary" style="margin: 0 8px 0 8px">Cancel</button></div>';
    toastr.warning(msg);
};

// delete confirmation
finance.toastrConfirmDelete = function(event, url) {
    if (url == null) {
        return;
    }
    event.preventDefault();
    var msg = '<div>Are you sure?</div><div><a id="okBtn" class="btn btn-danger"' + url + '">Yes</a>' +
        '<button type="button" id="btnCancel" class="btn btn-primary" style="margin: 0 8px 0 8px">Cancel</button></div>';
    toastr.warning(msg);
};

// draw 2d pie chart
finance.drawPieChart = function(data, elementId) {
    var pieData = [];
    for(var item in data){
    	pieData.push({
			value: data[item],
			color: randomColor(),
			label: item,
		});
	}
	var ctx = document.getElementById(elementId).getContext("2d");
	window.myPie = new Chart(ctx).Pie(pieData);
};