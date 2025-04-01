import { useState } from "react";

const Sidebar = () => {
  const [expandedMenus, setExpandedMenus] = useState<number[]>([]);

  const menuItems = [
    {
      id: 1,
      title: 'Dashboard',
      icon: <i className='bx bx-bar-chart-alt-2'></i>,
      path: '/dashboard'
    },
    {
      id: 2,
      title: 'User Management', 
      icon: <i className='bx bx-user'></i>,
      submenu: [
        { id: 21, title: 'Users List', path: '/users' },
        { id: 22, title: 'Roles', path: '/roles' },
        { id: 23, title: 'Permissions', path: '/permissions' }
      ]
    },
    {
      id: 3,
      title: 'Content',
      icon: <i className='bx bx-book'></i>, 
      submenu: [
        { id: 31, title: 'Posts', path: '/posts' },
        { id: 32, title: 'Categories', path: '/categories' },
        { id: 33, title: 'Tags', path: '/tags' }
      ]
    },
    {
      id: 4,
      title: 'Settings',
      icon: <i className='bx bx-cog'></i>, 
      submenu: [
        { id: 41, title: 'General', path: '/settings/general' },
        { id: 42, title: 'Security', path: '/settings/security' },
        { id: 43, title: 'Notifications', path: '/settings/notifications' }
      ]
    },
    {
      id: 5,
      title: 'eCommerce',
      icon: <i className='bx bx-shopping-bag'></i>,
      submenu: [
        { id: 51, title: 'Product List', path: '/ecommerce/product-list' },
      ]
    },
  ];

  const toggleMenu = (menuId:number) => {
    setExpandedMenus(prev =>
      prev.includes(menuId)
        ? prev.filter(id => id !== menuId)
        : [...prev, menuId]
    );
  };

  const isMenuExpanded = (menuId:number) => expandedMenus.includes(menuId);

  return (
    <aside className="w-64 h-screen bg-gradient-to-b from-gray-900 to-gray-800 text-gray-100 fixed left-0 top-0 overflow-y-auto shadow-xl">
      <div className="p-6">
        <h2 className="text-2xl font-bold mb-8 text-center text-transparent bg-clip-text bg-gradient-to-r from-blue-400 to-purple-500">
          Dashboard
        </h2>
        <nav>
          <ul className="space-y-3">
            {menuItems.map((item) => (
              <li key={item.id} className="group">
                {item.submenu ? (
                  <div>
                    <button
                      onClick={() => toggleMenu(item.id)}
                      className={`w-full flex items-center justify-between p-3 rounded-lg hover:bg-gray-700/50 backdrop-blur-sm transition-all duration-300 ${
                        isMenuExpanded(item.id) ? 'bg-gray-700/50 shadow-lg' : ''
                      }`}
                    >
                      <div className="flex items-center">
                        <span className="mr-3 text-xl group-hover:scale-110 transition-transform duration-300">{item.icon}</span>
                        <span className="font-medium">{item.title}</span>
                      </div>
                      <span className={`transform transition-transform duration-300 ${
                        isMenuExpanded(item.id) ? 'rotate-180' : ''
                      }`}>
                        ▼
                      </span>
                    </button>
                    {isMenuExpanded(item.id) && (
                      <ul className="ml-8 mt-2 space-y-2 border-l-2 border-gray-700 pl-4">
                        {item.submenu.map((subItem) => (
                          <li key={subItem.id}>
                            <a 
                              href={subItem.path}
                              className="block py-2 px-3 rounded-md hover:bg-gray-700/30 transition-colors duration-200 text-gray-300 hover:text-white"
                            >
                              {subItem.title}
                            </a>
                          </li>
                        ))}
                      </ul>
                    )}
                  </div>
                ) : (
                  <button
                    onClick={() => console.log(`Navigating to ${item.path}`)}
                    className="w-full flex items-center p-3 rounded-lg hover:bg-gray-700/50 backdrop-blur-sm transition-all duration-300"
                  >
                    <span className="mr-3 text-xl group-hover:scale-110 transition-transform duration-300">{item.icon}</span>
                    <span className="font-medium">{item.title}</span>
                  </button>
                )}
              </li>
            ))}
          </ul>
        </nav>
      </div>
    </aside>
  );
}

export default Sidebar;