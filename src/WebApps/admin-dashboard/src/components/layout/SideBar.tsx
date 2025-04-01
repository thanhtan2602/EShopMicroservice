import { useState } from "react";

const  Sidebar = () => {
  const [expandedMenus, setExpandedMenus] = useState<number[]>([]);

  const menuItems = [
    {
      id: 1,
      title: 'Dashboard',
      icon: '📊',
      path: '/dashboard'
    },
    {
      id: 2,
      title: 'User Management',
      icon: '👥',
      submenu: [
        { id: 21, title: 'Users List', path: '/users' },
        { id: 22, title: 'Roles', path: '/roles' },
        { id: 23, title: 'Permissions', path: '/permissions' }
      ]
    },
    {
      id: 3,
      title: 'Content',
      icon: '📝',
      submenu: [
        { id: 31, title: 'Posts', path: '/posts' },
        { id: 32, title: 'Categories', path: '/categories' },
        { id: 33, title: 'Tags', path: '/tags' }
      ]
    },
    {
      id: 4,
      title: 'Settings',
      icon: '⚙️',
      submenu: [
        { id: 41, title: 'General', path: '/settings/general' },
        { id: 42, title: 'Security', path: '/settings/security' },
        { id: 43, title: 'Notifications', path: '/settings/notifications' }
      ]
    },
    {
      id: 5,
      title: 'eCommerce',
      icon: '👥',
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
    <aside className="w-64 h-screen bg-gray-800 text-white fixed left-0 top-0 overflow-y-auto">
      <div className="p-4">
        <h2 className="text-xl font-bold mb-6">Menu</h2>
        <nav>
          <ul className="space-y-2">
            {menuItems.map((item) => (
              <li key={item.id}>
                {item.submenu ? (
                  // Menu item with submenu
                  <div>
                    <button
                      onClick={() => toggleMenu(item.id)}
                      className={`w-full flex items-center justify-between p-2 rounded hover:bg-gray-700 transition-colors ${isMenuExpanded(item.id) ? 'bg-gray-700' : ''
                        }`}
                    >
                      <div className="flex items-center">
                        <span className="mr-2">{item.icon}</span>
                        <span>{item.title}</span>
                      </div>
                      <span className={`transform transition-transform ${isMenuExpanded(item.id) ? 'rotate-180' : ''
                        }`}>
                        ▼
                      </span>
                    </button>
                    {isMenuExpanded(item.id) && (
                      <ul className="ml-6 mt-2 space-y-1">
                        {item.submenu.map((subItem) => (
                          <li key={subItem.id}>
                            <a href={subItem.path}>{subItem.title}</a>
                          </li>
                        ))}
                      </ul>
                    )}
                  </div>
                ) : (
                  // Single menu item
                  <button
                    onClick={() => console.log(`Navigating to ${item.path}`)}
                    className="w-full flex items-center p-2 rounded hover:bg-gray-700 transition-colors"
                  >
                    <span className="mr-2">{item.icon}</span>
                    <span>{item.title}</span>
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