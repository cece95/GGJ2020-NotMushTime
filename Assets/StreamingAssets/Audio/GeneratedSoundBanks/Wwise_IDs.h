/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID BOX_DRAGGING = 1592165308U;
        static const AkUniqueID DIALOGUE_SHROOMS = 2598333881U;
        static const AkUniqueID INTERACTIVE_MUSIC = 3734989563U;
        static const AkUniqueID MAIN_REACTOR = 658170097U;
        static const AkUniqueID PIPE_PUZZLE_FAIL = 1847720199U;
        static const AkUniqueID PIPE_PUZZLE_MATCH = 544157162U;
        static const AkUniqueID SHROOM_BOUNCE = 1380635400U;
        static const AkUniqueID SHROOM_STEPS = 785153393U;
        static const AkUniqueID SIMON_SAYS_PUZZLE = 3147088319U;
        static const AkUniqueID TETRIS_PUZZLE_CLICKS = 1463418219U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace HAPPY_DAYS_ARE_OVER
        {
            static const AkUniqueID GROUP = 2017233915U;

            namespace STATE
            {
                static const AkUniqueID ALARM_SYREN = 1353553046U;
                static const AkUniqueID HAPPY_DAYS = 3498417625U;
            } // namespace STATE
        } // namespace HAPPY_DAYS_ARE_OVER

        namespace SIMON_SAYS_PUZZLE
        {
            static const AkUniqueID GROUP = 3147088319U;

            namespace STATE
            {
                static const AkUniqueID PUZZLE_A = 3821528377U;
                static const AkUniqueID PUZZLE_B = 3821528378U;
                static const AkUniqueID PUZZLE_C = 3821528379U;
                static const AkUniqueID PUZZLE_COMPLETE = 376835237U;
                static const AkUniqueID PUZZLE_D = 3821528380U;
                static const AkUniqueID PUZZLE_FAILED = 3956906079U;
            } // namespace STATE
        } // namespace SIMON_SAYS_PUZZLE

    } // namespace STATES

    namespace SWITCHES
    {
        namespace SIMON_SAYS_PUZZLE
        {
            static const AkUniqueID GROUP = 3147088319U;

            namespace SWITCH
            {
                static const AkUniqueID PUZZLE_A = 3821528377U;
                static const AkUniqueID PUZZLE_B = 3821528378U;
                static const AkUniqueID PUZZLE_C = 3821528379U;
                static const AkUniqueID PUZZLE_COMPLETED = 14018523U;
                static const AkUniqueID PUZZLE_D = 3821528380U;
                static const AkUniqueID PUZZLE_FAILED = 3956906079U;
            } // namespace SWITCH
        } // namespace SIMON_SAYS_PUZZLE

        namespace SURFACE_TYPE
        {
            static const AkUniqueID GROUP = 4064446173U;

            namespace SWITCH
            {
                static const AkUniqueID FLOOR = 1088209313U;
            } // namespace SWITCH
        } // namespace SURFACE_TYPE

    } // namespace SWITCHES

    namespace TRIGGERS
    {
        static const AkUniqueID SHROOM_STEPS = 785153393U;
    } // namespace TRIGGERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID AMBIENCE = 85412153U;
        static const AkUniqueID INTERACTIVE_MUSIC = 3734989563U;
        static const AkUniqueID PUZZLES = 4237507684U;
        static const AkUniqueID SHROOMS = 3983930604U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID AMBIENCE = 85412153U;
        static const AkUniqueID INTERACTIVE_MUSIC = 3734989563U;
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID PUZZLES = 4237507684U;
        static const AkUniqueID SHROOMS = 3983930604U;
        static const AkUniqueID UI_PUZZLES = 2423156673U;
    } // namespace BUSSES

    namespace AUX_BUSSES
    {
        static const AkUniqueID REVERB = 348963605U;
    } // namespace AUX_BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
