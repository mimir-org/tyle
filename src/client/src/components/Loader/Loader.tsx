import { MotionFlexbox, Spinner } from "@mimirorg/component-library";
import { useTheme } from "styled-components";

/**
 * A simple wrapper for the spinner component which adds fading and centers it within a flexbox
 *
 * @constructor
 */
const Loader = () => {
  const theme = useTheme();

  return (
    <MotionFlexbox flex={1} justifyContent={"center"} alignItems={"center"} {...theme.mimirorg.animation.fade}>
      <Spinner disabled={false} />
    </MotionFlexbox>
  );
};

export default Loader;
