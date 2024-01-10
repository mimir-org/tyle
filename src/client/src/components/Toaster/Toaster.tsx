import { AnimatePresence } from "framer-motion";
import * as ReactHotToast from "react-hot-toast";
import { useTheme } from "styled-components";
import { getCustomToastBarStyles, getCustomToasterStyles } from "./Toaster.helpers";
import { MotionToastBarWrapper } from "./Toaster.styled";

/**
 * Wrapper around react-hot-toast's provider.
 * Place this alongside the application's root to utilize the libraries configurations and styles for toasts.
 *
 * See documentation link below for details.
 * @see https://react-hot-toast.com/docs
 *
 * @constructor
 */
const Toaster = () => {
  const theme = useTheme();
  const customToasterStyles = getCustomToasterStyles(theme.tyle);
  const customToastBarStyles = getCustomToastBarStyles();

  return (
    <ReactHotToast.Toaster position={"bottom-right"} toastOptions={customToasterStyles}>
      {(toast) => (
        <AnimatePresence>
          {toast.visible && (
            <MotionToastBarWrapper {...theme.tyle.animation.from("right", 400)}>
              <ReactHotToast.ToastBar toast={toast} style={customToastBarStyles} />
            </MotionToastBarWrapper>
          )}
        </AnimatePresence>
      )}
    </ReactHotToast.Toaster>
  );
};

export default Toaster;
