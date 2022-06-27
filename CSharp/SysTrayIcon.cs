    class SysTrayIcon : IDisposable
    {
        IContainer container;
        ContextMenu context;
        NotifyIcon notifyicon;

        /// <summary>
        /// Initialize the system tray icon
        /// </summary>
        /// <param name="title">System tray icon display title</param>
        /// <param name="iconPath">icon file path (NOT OPTIONAL!)</param>
        public void Init(string title, string iconPath)
        {
            container = new Container();
            context = new ContextMenu();
            notifyicon = new NotifyIcon(container);

            notifyicon.Icon = new Icon(iconPath);
            notifyicon.ContextMenu = context;

            notifyicon.Text = title;
            notifyicon.Visible = true;

            notifyicon.DoubleClick += (object sender, EventArgs e) => {};
            notifyicon.Click += (object sender, EventArgs e) => {};
        }

        /// <summary>
        /// Add a text node to the system tray icon context menu
        /// </summary>
        /// <param name="nodeText">node display text</param>
        /// <param name="onClicked">node onclick function</param>
        public void AddNode(string nodeText, Action onClicked)
        {
            MenuItem contextNode = new MenuItem();
            contextNode.Text = nodeText;
            contextNode.Index = context.MenuItems.Count;
            contextNode.Click += (object sender, EventArgs e) => { onClicked(); };
            context.MenuItems.Add(contextNode);
        }

        /// <summary>
        /// Throw the application into its run loop
        /// </summary>
        public void ApplicationLoop()
        {
            Application.Run();
        }

        /// <summary>
        /// Dispose of all the system tray icon shit
        /// </summary>
        public void Dispose()
        {
            notifyicon.Dispose();
            context.Dispose();
            container.Dispose();
        }
    }

/*

Usage:
 
    class Program
    {
        static void Main(string[] args)
        {
            SysTrayIcon icon = new SysTrayIcon();
            icon.Init("A2 Utils", "icon.ico"); // initialize the tray icon

            icon.AddNode("Exit", Application.Exit); // add some nodes

            icon.ApplicationLoop(); // initialize the application loop

            icon.Dispose(); // dispose of the tray shit so it doesnt stick around after the program closes
        }
    }

*/
