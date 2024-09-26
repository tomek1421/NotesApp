import { createContext, useContext, useEffect, useRef } from 'react';
import { useLocation } from 'react-router-dom';

// Create a context to store the previous location
const PreviousLocationContext = createContext(null);

export const PreviousLocationProvider = ({ children }) => {
  const location = useLocation();
  const prevLocationRef = useRef(location);

  useEffect(() => {
    prevLocationRef.current = location; // Update previous location when route changes
  }, [location]);

  return (
    <PreviousLocationContext.Provider value={prevLocationRef.current}>
      {children}
    </PreviousLocationContext.Provider>
  );
};

// Hook to access previous location from context
export const usePreviousLocation = () => {
  return useContext(PreviousLocationContext);
};
