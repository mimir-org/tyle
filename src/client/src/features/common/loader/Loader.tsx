import { Spinner } from "complib/feedback";
import { MotionFlexbox } from "@mimirorg/component-library";
import { useTheme } from "styled-components";

/**
 * A simple wrapper for the spinner component which adds fading and centers it within a flexbox
 *
 * @constructor
 */
export const Loader = () => {
  const theme = useTheme();

  return (
    <MotionFlexbox flex={1} justifyContent={"center"} alignItems={"center"} {...theme.tyle.animation.fade}>
      <Spinner />
    </MotionFlexbox>
  );
};
