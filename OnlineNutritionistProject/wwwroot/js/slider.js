// Hero Slider JavaScript
let currentSlideIndex = 0;
const slides = document.querySelectorAll('.slide');
const indicators = document.querySelectorAll('.indicator');
const totalSlides = slides.length;

// Initialize slider
function initSlider() {
    showSlide(0);
    startAutoSlide();
}

// Show specific slide
function showSlide(index) {
    // Remove active class from all slides and indicators
    slides.forEach(slide => slide.classList.remove('active'));
    indicators.forEach(indicator => indicator.classList.remove('active'));
    
    // Add active class to current slide and indicator
    slides[index].classList.add('active');
    indicators[index].classList.add('active');
    
    currentSlideIndex = index;
}

// Next slide
function nextSlide() {
    const nextIndex = (currentSlideIndex + 1) % totalSlides;
    showSlide(nextIndex);
}

// Previous slide
function prevSlide() {
    const prevIndex = (currentSlideIndex - 1 + totalSlides) % totalSlides;
    showSlide(prevIndex);
}

// Change slide (called by buttons)
function changeSlide(direction) {
    if (direction === 1) {
        nextSlide();
    } else {
        prevSlide();
    }
    resetAutoSlide();
}

// Current slide (called by indicators)
function currentSlide(index) {
    showSlide(index - 1);
    resetAutoSlide();
}

// Auto slide functionality
let autoSlideInterval;

function startAutoSlide() {
    autoSlideInterval = setInterval(nextSlide, 5000); // 5 seconds
}

function resetAutoSlide() {
    clearInterval(autoSlideInterval);
    startAutoSlide();
}

// Touch/Swipe functionality for mobile
let touchStartX = 0;
let touchEndX = 0;

function handleTouchStart(e) {
    touchStartX = e.changedTouches[0].screenX;
}

function handleTouchEnd(e) {
    touchEndX = e.changedTouches[0].screenX;
    handleSwipe();
}

function handleSwipe() {
    const swipeThreshold = 50;
    const diff = touchStartX - touchEndX;
    
    if (Math.abs(diff) > swipeThreshold) {
        if (diff > 0) {
            // Swipe left - next slide
            nextSlide();
        } else {
            // Swipe right - previous slide
            prevSlide();
        }
        resetAutoSlide();
    }
}

// Keyboard navigation
function handleKeyPress(e) {
    if (e.key === 'ArrowLeft') {
        prevSlide();
        resetAutoSlide();
    } else if (e.key === 'ArrowRight') {
        nextSlide();
        resetAutoSlide();
    }
}

// Initialize when DOM is loaded
document.addEventListener('DOMContentLoaded', function() {
    if (slides.length > 0) {
        initSlider();
        
        // Add touch event listeners
        const slider = document.querySelector('.hero-slider');
        if (slider) {
            slider.addEventListener('touchstart', handleTouchStart, false);
            slider.addEventListener('touchend', handleTouchEnd, false);
        }
        
        // Add keyboard event listener
        document.addEventListener('keydown', handleKeyPress);
        
        // Pause auto-slide when user hovers over slider
        const sliderSection = document.querySelector('.hero-slider-section');
        if (sliderSection) {
            sliderSection.addEventListener('mouseenter', () => {
                clearInterval(autoSlideInterval);
            });
            
            sliderSection.addEventListener('mouseleave', () => {
                startAutoSlide();
            });
        }
    }
});

// Pause auto-slide when page is not visible
document.addEventListener('visibilitychange', function() {
    if (document.hidden) {
        clearInterval(autoSlideInterval);
    } else {
        startAutoSlide();
    }
}); 