
          let slideIndex = 1;
          //sayfa açıldıgında ilk çalışan degişken let slideIndex = 1;oluyor
          showSlides(slideIndex);
          var $loupe = $(".loupe"),
          loupeWidth = $loupe.width(),
          loupeHeight = $loupe.height();
         
   $(document).on("mouseenter", ".images", function (e) {
        var $currImage = $(this),
         $img = $('<img />')
         .attr('src', $('img', this).attr("src"))
         .css({'width': $currImage.width() * 2, 'height': $currImage.height() * 2 });
         $loupe.html($img).fadeIn(5);

         $(document).on("mousemove", moveHandler);
         function moveHandler(e) {
                 var imageOffset = $currImage.offset(),
         fx = imageOffset.left - loupeWidth / 2,
         fy = imageOffset.top - loupeHeight / 2,
         fh = imageOffset.top + $currImage.height() + loupeHeight / 2,
         fw = imageOffset.left + $currImage.width() + loupeWidth / 2;
        
         $loupe.css({
             'left': e.pageX -500,
             'top': e.pageY -330
         });
        
          var loupeOffset =
          $loupe.offset(),
         
          lx = loupeOffset.left,
          ly = loupeOffset.top,
          lw = lx + loupeWidth,
          lh = ly + loupeHeight,
          bigy = (ly - loupeHeight / 4 - fy) * 2,
          bigx = (lx - loupeWidth / 4 - fx) * 2;
         
          $img.css({'left': -bigx, 'top': -bigy });

            if (lx < fx || lh > fh || ly < fy || lw > fw) {
            $img.remove();
            $(document).off("mousemove", moveHandler);
            $loupe.fadeOut(50);
            }
           }
       });
       //bu foksiyon küçük resimlere tıklanınca
       function currentSlide(n) {
           showSlides(slideIndex = n);
      
       }

       function showSlides(n) {
       
       let i;
       let slides = document.getElementsByClassName("mySlides");
       let dots = document.getElementsByClassName("demo");
       let captionText = document.getElementById("caption");
       
       for (i = 0; i < slides.length; i++) {
           slides[i].style.display = "none";
           }
       
       //bu büyük resmi gösteriyor
       slides[slideIndex -1].style.display = "block";
       
           console.log("slides, slideIndex", slides, slideIndex);

           //D) Resmin Adını Alıp Caption Olarak Yazdırma
      // Bu kod, büyük resmin dosya adını alıp sayfadaki < p id = "caption" > </p> içine yazıyor.
      let fullPath = slides[slideIndex-1].getElementsByTagName("img")[0].src;
      let fileName = fullPath.substring(fullPath.lastIndexOf('/') + 1);
      fileName = fileName.substring(0, fileName.lastIndexOf('.')); // Uzantıyı kaldır
      captionText.innerHTML = fileName;// Sadece dosya adını al

       }
       
       

