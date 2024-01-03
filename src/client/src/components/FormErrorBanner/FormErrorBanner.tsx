import Box from "components/Box";
import { ReactNode } from "react";
import { useTheme } from "styled-components";

interface FormErrorBannerProps {
  children?: ReactNode;
}

/**
 * Banner for displaying global error information in forms
 */
const FormErrorBanner = ({ children }: FormErrorBannerProps) => {
  const theme = useTheme();

  return (
    <Box
      {...theme.tyle.animation.fade}
      spacing={{ p: theme.tyle.spacing.l }}
      bgColor={theme.tyle.color.error.base}
      color={theme.tyle.color.error.on}
    >
      {children}
    </Box>
  );
};

export default FormErrorBanner;
