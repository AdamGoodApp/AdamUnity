using UnityEngine;
using System.Collections;
using ThinksquirrelSoftware.Common;

/// <summary>
/// Thinkscroller namespace.
/// </summary>
namespace ThinksquirrelSoftware.Thinkscroller
{
	/// <summary>
	/// Represents the axes that are used for parallax scrolling.
	/// </summary>
	public enum ScrollConstraints
	{
		X = 0,
		Y = 1,
		XY = 2
	}
	
	/// <summary>
	/// Represents the screen alignment of a scroll layer.
	/// </summary>
	public enum ScrollLayerAlignment
	{
		TopLeft,
		Top,
		TopRight,
		Left,
		Center,
		Right,
		BottomLeft,
		Bottom,
		BottomRight
	}
	
	/// <summary>
	/// MonoBehaviour base class for all Thinkscroller components. 
	/// </summary>
	public abstract class ThinkscrollerBase : MonoBase
	{
	}
	
#region Doxygen information

/*! \mainpage Reference Manual
 *
 * \section overview_sec Introduction
 *
 * Welcome to the \htmlonly Thinkscroller \endhtmlonly documentaion. The following pages contain a /ref guide and /ref components. Information about the \htmlonly Thinkscroller \endhtmlonly API is available as well.
 *
 * \subpage guide
 * \li \ref installation
 * \li \ref create
 * \li \ref modes
 * \li \ref animation
 * \li \ref examples
 * 
 * \subpage tutorial
 * \li \ref tutorial1
 * \li \ref tutorial2
 * 
 * \subpage advanced
 * \li \ref objectlayerworkflow
 * 
 * \subpage components
 * \li Manager
 * - \ref parallaxmanager
 * \li Scroll Layer
 * - \ref scroll_layer_a
 * - \ref scroll_layer_b
 * - \ref scroll_layer_c
 * \li Other
 * - \ref scroll_layer_animation
 * 
 * \subpage changelog
 * 
 * \section links Links
 * \if beta
 * \li Online documentation link: http://support.thinksquirrel.com/docs/thinkscroller-beta/reference
 * \endif
 * \if release
 * \li Online documentation link: http://support.thinksquirrel.com/docs/thinkscroller/reference
 * \endif
 * \li You can contact Thinksquirrel Software at: http://thinksquirrel.com/contact
 * \li Thinksquirrel Software Forum: http://thinksquirrel.com/forum
 */

/*! \page guide Getting Started Guide
 * 
 * Welcome to the Getting Started Guide! Please click a link to proceed:
 * \li \subpage installation
 * \li \subpage create
 * \li \subpage modes
 * \li \subpage animation
 * \li \subpage examples
 * 
 */

/*! \page tutorial Thinkscroller Tutorial Series
 * 
 * The following YouTube videos are intended to familiarize users with \htmlonly Thinkscroller.\endhtmlonly New tutorials are on the way!
 *
 * \li \subpage tutorial1
 * \li \subpage tutorial2
 */
	
/*! \page advanced Advanced Topics
 * 
 * \li \subpage objectlayerworkflow
 * 
 */

/*! \page installation Installation
 *
 * \image html thinkscrollerinstall.png
 * \htmlinclude INSTALL.txt
 * 
 * \subsection Navigation
 * 
 * \li Next: \ref create
 * 
 */

/*! \page create Creating a Parallax Scene
 *
 * In order to create a parallax scene, go to <b>Thinksquirrel > Thinkscroller > Create Parallax Scene</b>.
 * This will create all the necessary objects and components that you will need, with a setup similar to the example scenes. You may need to hit play to get the scene to render correctly.
 * \image html thinkscrollercreatemenu.png
 * \note If you select an orthographic camera when running this menu command, Thinkscroller will use that camera as the parallax camera. Otherwise, it will search for an orthographic main camera - if none is found, it will create a new parallax camera.
 * 
 * You will also need to add a script that uses the function Parallax.Scroll in order to actually scroll. The example project contains two scrolling examples: <b>Component > Thinkscroller Example Project > Constant Scroll</b> and <b>Component > Thinkscroller Example Project > Scroll With Transform</b> - attach these to your camera.
 * 
 * \subsection Navigation
 *
 * \li Back: \ref installation
 * \li Next: \ref modes
 * 
 */

/*! \page modes Scroll Layers and Scroll Layer Modes
 *
 * \htmlonly Thinkscroller\endhtmlonly has three modes of operation:
 * 
 * \section Auto-Billboard Layer
 * 
 * \image html sceneviewauto.png
 * This is the default mode of operation. It automatically creates a billboard (camera-facing mesh) and scrolls the UVs of a texture placed on it. Only orthographic cameras are officially supported in this mode.
 * \see \ref scroll_layer_a
 * 
 * \section Manual UV Scrolling Layer
 * 
 * \image html sceneviewmanual.png
 * If auto-billboard is disabled, \htmlonly Thinkscroller\endhtmlonly can scroll the textures on a mesh. To use this mode, disable auto-billboard, and then specify a mesh renderer to use. This mode can be used to scroll on custom meshes, or scroll multiple textures (in the same shader) at the same time, like normal maps or mask textures.
 * \see \ref scroll_layer_b
 * 
 * \section Object Layer
 * 
 * \image html sceneviewobject.png
 * This is commonly used with a sprite management package. Object layers scroll the transform of an object instead of the UVs of a texture. As such, it is up to the user to manage any sprite tiling. A common workflow is to set up a sprite based layer under a parent object, and use an object layer to scroll the parent.
 * \see \ref scroll_layer_c
 * 
 * \subsection Navigation
 *
 * \li Back: \ref create
 * \li Next: \ref examples
 * 
 */
	
/*! \page animation Animating Scroll Layers
 * 
 * 
 * Scroll Layers can be animated using animation curves. First, add the \ref scroll_layer_animation component (<b>Component > Thinkscroller > Scroll Layer Animation</b>).
 *
 * \image html animationcurves.png
 * Layer weight, speed, offsets, and padding can all be animated.
 * 
 * \subsection Navigation
 *
 * \li Back: \ref modes
 * \li Next: \ref examples
 */
	
/*! \page examples Example Project
 * 
 * \section Overview
 * 
 * \par
 * 
 * The \htmlonly Thinkscroller\endhtmlonly Example Project folder contains a few example scenes for different scenarios:
 * 
 * \section example_1 ThinkscrollerExample
 *
 * \image html example_1.png
 * The first example scene shows the default pixel-perfect scroll layer setup, with 3 equally-sized layers.
 * 
 * \section example_2 ThinkscrollerExample2
 *
 * \image html example_2.png
 * The second example scene contains many more scroll layers and shows how many layers can be placed in a scene.
 * 
 * \section example_3 ThinkscrollerExample3
 * 
 * \image html example_3.png
 * The third example scene uses cropped scroll layers, and provides a control for changing the scroll speed. 
 * 
 * \section example_4 ThinkscrollerExample4
 * 
 * \image html example_4.png
 * The fourth example scene is an advanced scene that shows how scroll vectors can be used for other things. In this scene, a raw scroll vector is used to rotate planet layers. 
 *
 * \subsection Navigation
 *
 * \li Back: \ref animation
 */

/*! \page tutorial1 Thinkscroller Tutorial Series #1: Getting Started / Feature Overview
 * 
 * \section Video
 * 
 * \htmlonly
 * <iframe width="640" height="480" src="http://www.youtube-nocookie.com/embed/MXfL0ADhHUg?html5=1" frameborder="0" allowfullscreen></iframe>
 * \endhtmlonly
 *
 * \note If viewing documentation within Unity: Make sure to pause this video before editing any scripts!
 * 
 * \section importexport_sec Description
 * 
 * This tutorial video outlines how to create a new parallax scene, as well as Thinkscroller's main feature set.
 * 
 * \subsection Navigation
 * 
 * \li Next: \ref tutorial2
 * 
 */

/*! \page tutorial2 Thinkscroller Tutorial Series #2: Making a Parallax Scrolling Character
 * 
 * \section Video
 * 
 * \htmlonly
 * <iframe width="640" height="480" src="http://www.youtube-nocookie.com/embed/IheSFFp9rRY?html5=1" frameborder="0" allowfullscreen></iframe>
 * \endhtmlonly
 * 
 * \note If viewing documentation within Unity: Make sure to pause this video before editing any scripts!
 * 
 * \section importexport_sec Description
 * 
 * This tutorial video outlines how to sync a parallax scene with a character.
 * 
 * \subsection Navigation
 * 
 * \li Back: \ref tutorial1
 * 
 */

/*! \page objectlayerworkflow Object Layer Workflow
 * 
 * 
 * Object layers should typically be parent objects for multiple layers of sprites.
 *
 * \section spritemanagement Sprite Management
 * 
 * \par
 * 
 * The following examples use NGUI (more information <a href=http://www.tasharen.com/?page_id=140>here</a>) to draw sprites. Any sprite management tool or even 3D objects can be used.
 * 
 * \section parentingobjectlayers Parenting Object Layers
 * 
 * \par
 * 
 * \image html objectlayerworkflow.png
 * In this image, sprites are placed down manually and a single object layer scrolls them all at the same speed.
 * 
 * \image html objectlayerworkflow2.png
 * Multiple object layers can then be combined in order to make a robust, manually placed scene that utilizes parallax scrolling.
 * 
 * \section repeatingobjectlayers Repeating Object Layers
 * 
 * \par
 * 
 * In order to repeat object layers infinitely, the transform will need to be reset after a certain distance. This value will need to be set based on your image and screen size so that no visual popping occurs. If you are trying to make repeating background layers, take a look at \ref scroll_layer_a "Auto-Billboard Scroll Layers".
 * 
 * \section demoproject Object Layer Demo Project
 * 
 * \par
 * 
 * You can download a seperate object layer demo project.
 * 
 * To install:
 * -# Create a new Unity Project
 * -# Download and install NGUI Free from <a href=http://dl.dropbox.com/u/33426743/temp-A8CEBAF24E/ngui_free.unitypackage>here</a>.
 * -# Download and install \htmlonly Thinkscroller\endhtmlonly from the Asset Store.
 * -# Download and install the demo project from <a href=http://dl.dropbox.com/u/33426743/ThinkscrollerDemo/thinkscrollerdemo.unitypackage>here</a>.
 * 
 * \subsection Navigation
 *
 * \li Back: \ref advanced
 */
	
/*! \page components Component Reference
 *
 * All Thinkscroller classes that are also a component (ScrollLayer, ParallaxManager, and ScrollLayerAnimation) have a wrapper class in the global namespace.
 * It is recommended to use these classes with AddComponent, GetComponent, and other Unity-related functions.
 *
 * 
 * \par
 * \section manager_sec Manager
 * \par
 * 
 * \li \subpage parallaxmanager
 * 
 * \par
 * \section scroll_layer_sec Scroll Layer
 * \par
 * 
 * \li \subpage scroll_layer_a
 * \li \subpage scroll_layer_b
 * \li \subpage scroll_layer_c
 * 
 * \par
 * \section other_sec Other
 * \par
 * 
 * \li \subpage scroll_layer_animation
 * 
 */
	
/*! \page changelog Changelog
 * 
 * \section changelog_current Current Version
 * \verbinclude CHANGELOG.txt
 * 
 */
#endregion

}
