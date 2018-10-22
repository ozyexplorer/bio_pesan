<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="PesanMakan.Views.index" %>
<%@ Import Namespace="PesanMakan.Presentation.Component" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Halaman Utama</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="../Content/index/bootstrap.min.css" rel="stylesheet"/>
    <link href="../Content/index/style.css" rel="stylesheet" />
    <script src="../Content/index/jquery.min.js"></script>
    <script src="../Content/index/jquery.magnific-popup.js"></script>

    <script>
        $(document).ready(function () {
            $('.popup-with-zoom-anim').magnificPopup({
                type: 'inline',
                fixedContentPos: false,
                fixedBgPos: true,
                overflowY: 'auto',
                closeBtnInside: true,
                preloader: false,
                midClick: true,
                removalDelay: 300,
                mainClass: 'my-mfp-zoom-in'
            });

        });
	</script>

	<!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="js/html5shiv.js"></script>
        <script src="js/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <!-- /////////////////////////////////////////Navigation -->
	<nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
		<div class="container">
			<div class="navbar-header">
				<button type="button" class="navbar-toggle" data-toggle="collapse"
						data-target="#bs-example-navbar-collapse-1">
					<span class="sr-only">Toggle navigation</span>
					<span class="icon-bar"></span>
					<span class="icon-bar"></span>
					<span class="icon-bar"></span>

				</button>
				<a class="navbar-brand" href="#">
					<img style="border-radius: 10%;" src="../images/biofarma_1.jpg" class="hidden-xs" alt="">
					<h3 class="visible-xs">Bio Farma</h3>
				</a>
			</div>
			<div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
				<ul class="nav navbar-nav navbar-right">
					<h5 style="font-weight: bold; color: white;">PESAN MAKAN APPS</h5>
				</ul>
			</div>
		</div>
	</nav>
	
	<header id="intro">
		<!-- Carousel -->
    	<div id="carousel-example-generic" class="carousel slide" data-ride="carousel">
			<!-- Indicators -->
			<ol class="carousel-indicators">
			  	<li data-target="#carousel-example-generic" data-slide-to="0" class="active"></li>
			    <li data-target="#carousel-example-generic" data-slide-to="1"></li>
			    <li data-target="#carousel-example-generic" data-slide-to="2"></li>
			</ol>
			<!-- Wrapper for slides -->
			<div class="carousel-inner">
			    <div class="item active">
			    	<img src="../images/1.jpg" alt="First slide"/>
                    <!-- Static Header -->
                    <div class="header-text">
                        <div class="col-md-12 text-center">
                            <h2>SELAMAT DATANG <br/>DI PESAN MAKAN APPS</h2>
                            <br>
                            <p style="color: white;">Silahkan pilih menu login yang diinginkan</p>
                            <br>
                            <div class="">
                                <a class="btn btn-1 btn-sm" href="Login.aspx">TAP CARD</a><a class="btn btn-1 btn-sm" href="UserLogin.aspx">USERNAME</a>
							</div>
                        </div>
                    </div><!-- /header-text -->
			    </div>
			    <div class="item">
			    	<img src="../images/2.jpg" alt="Second slide">
			    	<!-- Static Header -->
                    <div class="header-text">
                        <div class="col-md-12 text-center">
                            <h2>SELAMAT DATANG <br/>DI PESAN MAKAN APPS</h2>
                            <br>
                            <p style="color: white;">Silahkan pilih menu login yang diinginkan</p>
                            <br/>
                            <div class="">
                                <a class="btn btn-1 btn-sm" href="#about">TAP CARD</a><a class="btn btn-1 btn-sm" href="#works">USERNAME</a>
							</div>
                        </div>
                    </div><!-- /header-text -->
			    </div>
			    <div class="item">
			    	<img src="../images/3.jpg" alt="Third slide">
			    	<!-- Static Header -->
                    <div class="header-text hidden-xs">
                        <div class="col-md-12 text-center">
                            <h2>SELAMAT DATANG <br/>DI PESAN MAKAN APPS</h2>
                            <br>
                            <p style="color: white;">Silahkan pilih menu login yang diinginkan</p>
                            <br>
                            <div class="">
                                <a class="btn btn-1 btn-sm" href="Login.aspx">TAP CARD</a><a class="btn btn-1 btn-sm" href="UserLogin.aspx">USERNAME</a>
							</div>
                        </div>
                    </div><!-- /header-text -->
			    </div>
			</div>
			<!-- Controls -->
			<a class="left carousel-control" href="#carousel-example-generic" data-slide="prev">
		    	<span class="glyphicon glyphicon-chevron-left"></span>
			</a>
			<a class="right carousel-control" href="#carousel-example-generic" data-slide="next">
		    	<span class="glyphicon glyphicon-chevron-right"></span>
			</a>
		</div><!-- /carousel -->
	</header>
	<!-- Header -->

    <!-- Core JavaScript Files -->

    <script src="../Content/index/bootstrap.min.js"></script>
	<script>
	    $(document).ready(function () {
	        $('#backTop').backTop({
	            'position': 1200,
	            'speed': 500,
	            'color': 'red',
	        });
	    });
	</script>
	<script src="../Content/index/demo.js"></script>
</body>


</html>
