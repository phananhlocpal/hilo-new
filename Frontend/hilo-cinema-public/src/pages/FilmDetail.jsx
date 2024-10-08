<<<<<<< HEAD
﻿// Import libraries
import { useState, useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useParams } from "react-router-dom";

// Import components
import Schedule from "../components/movieDetail_components/Schedule";
import MovieChooseDateComponent from "../components/movieDetail_components/MovieChooseDateComponent";
import MovieContent from "../components/movieDetail_components/MovieContent.jsx";
import MovieOverviewComponent from "../components/movieDetail_components/MovieOverviewComponent.jsx";
import MovieSuggestionComponent from "../components/common_components/MovieSuggestionComponent.jsx";

// Import action
import { fetchMovie } from '../redux/actions/movieDetail/movieDetailAction.js';
=======
﻿import Schedule from "../components/movieDetail_components/Schedule";
import MovieChooseDateComponent from "../components/movieDetail_components/MovieChooseDateComponent";
import { exampleSchedule } from '../../data_example.js';
import { useState, useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { fetchMovie } from '../redux/actions/movieDetail/movieDetailAction.js';
import MovieContent from "../components/movieDetail_components/MovieContent.jsx";
import MovieOverviewComponent from "../components/movieDetail_components/MovieOverviewComponent.jsx";
import MovieSuggestionComponent from "../components/common_components/MovieSuggestionComponent.jsx";
import { useParams } from "react-router-dom";
>>>>>>> 960a83c (commit)

const FilmDetail = () => {
    const { movieUrl } = useParams();
    const dispatch = useDispatch();
    const { movie, loading, error } = useSelector((state) => state.movieDetail);
<<<<<<< HEAD
    const { schedule } = useSelector((state) => state.schedule);
=======
    const { schedule } = useSelector((state) => state.schedule); 
>>>>>>> 960a83c (commit)
    const [selectedDate, setSelectedDate] = useState('');
    const [dataLoaded, setDataLoaded] = useState(false);

    useEffect(() => {
        const fetchData = async () => {
            try {
                await dispatch(fetchMovie(movieUrl));
<<<<<<< HEAD
                setDataLoaded(true);
            } catch (err) {
                console.error("Error fetching movie:", err);
                setDataLoaded(false);
=======
                setDataLoaded(true); 
            } catch (err) {
                console.error("Error fetching movie:", err);
                setDataLoaded(false); 
>>>>>>> 960a83c (commit)
            }
        };

        fetchData();
    }, [dispatch, movieUrl]);

    useEffect(() => {
<<<<<<< HEAD
        if (schedule && schedule.length > 0) {
            setSelectedDate(schedule[0].date);
        }
        console.log(schedule)
=======
        if (schedule.schedules && schedule.schedules.length > 0) {
            setSelectedDate(schedule.schedules[0].date); 
        }
>>>>>>> 960a83c (commit)
    }, [schedule]);

    const handleDateChange = (newDate) => {
        setSelectedDate(newDate);
    };

    if (loading) return <p>Loading...</p>;
    if (error) return <p>Error loading movie details</p>;

    return (
        <div>
            <div className="book__ticket__wrapper">
                <div className="relative bg-black flex justify-center w-full h-full">
                    {/* Movie Image and Play Button */}
                    <div className="relative h-full">
                        <img
                            alt="Img Movie"
                            loading="lazy"
                            className="w-[860px] h-full md:h-full lg:h-[500px] object-fill"
<<<<<<< HEAD
                            src={movie?.imgLarge ? `data:image/jpeg;base64,${movie.imgLarge}` : 'https://via.placeholder.com/1440x440'}
=======
                            src={movie?.imgLarge ? `data:image/jpeg;base64,${movie.imgLarge}` : 'https://via.placeholder.com/1440x440'}  
>>>>>>> 960a83c (commit)
                        />
                        <button className="absolute top-[50%] left-[50%] -translate-x-2/4 -translate-y-2/4 z-[600]">
                            <img
                                alt="play"
                                loading="lazy"
                                className="w-[40px] h-[40px] lg:w-[64px] lg:h-[64px] object-cover"
                                src="https://www.galaxycine.vn/_next/static/media/button-play.2f9c0030.png"
                            />
                        </button>
                    </div>
                </div>
                <div className="grid grid-cols-1 screen1200:grid-cols-7 my-0 mx-auto screen1390:max-w-screen-xl xl:max-w-screen-screen1200 lg:max-w-4xl md:max-w-4xl gap-8 py-7 md:px-4 px-4">
                    <div className="book__left lg:col-span-5 w-full ">
                        <div className="book__film flex flex-col">
                            {dataLoaded && (
                                <>
                                    <MovieOverviewComponent movie={movie} />
                                    <MovieContent movieModel={movie} />
                                </>
                            )}
                            <div className="movie__showtime">
                                <div className="movie__showtime-header">
                                    <span className="border-l-4 border-solid border-blue-10 mr-2"></span>
                                    <h1 className="mb-4 text-base inline-block capitalize font-bold">Lịch chiếu</h1>
                                </div>
                                <MovieChooseDateComponent
<<<<<<< HEAD
                                    schedule={schedule}
                                    selectedDate={selectedDate}
                                    onDateChange={handleDateChange}
                                />
                                {dataLoaded && movie && (
                                    <Schedule movie={movie} selectedDate={selectedDate} />
                                )}
=======
                                    schedule={schedule.MovieSchedule} 
                                    selectedDate={selectedDate}
                                    onDateChange={handleDateChange}
                                />
                                <Schedule movieUrl={movieUrl} selectedDate={selectedDate} /> {/* Truyền selectedDate vào Schedule */}
>>>>>>> 960a83c (commit)
                            </div>
                        </div>
                    </div>
                    <MovieSuggestionComponent />
                </div>
            </div>
        </div>
    );
};

export default FilmDetail;