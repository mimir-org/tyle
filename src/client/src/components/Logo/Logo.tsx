import { Icon, IconProps, usePrefersTheme } from "@mimirorg/component-library";
import { TyleLogoDarkRedIcon, TyleLogoWhiteIcon } from "components/Logo/logos";
import { motion } from "framer-motion";
import { ForwardedRef, forwardRef } from "react";

type LogoProps = Omit<IconProps, "src"> & {
  inverse?: boolean;
};

/**
 * Logo component which wraps actual the logo SVGs providing the source which matches the current theme.
 *
 * @param inverse if true will deliver "light" logo while in dark theme and the opposite for the light theme
 * @param delegated receives all props available to Icon components (IconProps)
 * @constructor
 */
const Logo = forwardRef((props: LogoProps, ref: ForwardedRef<HTMLImageElement>) => {
  let [theme] = usePrefersTheme("tyleLight");
  const { inverse, ...delegated } = props;

  if (inverse && theme === "tyleLight") theme = "tyleDark";
  else if (inverse && theme === "tyleDark") theme = "tyleLight";

  return theme === "tyleDark" ? (
    <Icon ref={ref} src={TyleLogoDarkRedIcon} {...delegated} />
  ) : (
    <Icon ref={ref} src={TyleLogoWhiteIcon} {...delegated} />
  );
});

export default Logo;

Logo.displayName = "Logo";

/**
 * An animation wrapper for the Logo component
 *
 * @see https://github.com/framer/motion
 */
export const MotionLogo = motion(Logo);
