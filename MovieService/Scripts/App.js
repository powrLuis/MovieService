var ViewModel = function () {
    var self = this;
    self.movies = ko.observableArray();
    self.error = ko.observable();
    self.newMovie = {
        Titulo: ko.observable(),
        Año: ko.observable(),
        Precio: ko.observable()
    }
    var moviesUri = '/api/movies/';

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getAllMovies() {
        ajaxHelper(moviesUri, 'GET').done(function (data) {
            self.movies(data);
        });
    }

    // Fetch the initial data.
    getAllMovies();

    self.detail = ko.observable();

    self.getMovieDetail = function (item) {
        ajaxHelper(moviesUri + item.Id, 'GET').done(function (data) {
            self.detail(data);
        });
    }

    self.AddMovie = function (formElement) {
        var movie = {
            Titulo: self.newMovie.Titulo(),
            Año: self.newMovie.Año(),
            Precio: self.newMovie.Precio()
        };
        ajaxHelper(moviesUri, 'POST', movie).done(function (item) {
            self.movies.push(item);
        });
    }

};

ko.applyBindings(new ViewModel());