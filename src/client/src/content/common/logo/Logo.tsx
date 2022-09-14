import { motion } from "framer-motion";
import { ForwardedRef, forwardRef } from "react";
import { TyleLogoDarkRedIcon, TyleLogoWhiteIcon } from "../../../assets/icons/logo";
import { Icon } from "../../../complib/media";
import { IconProps } from "../../../complib/media/icon/Icon";
import { usePrefersTheme } from "../../../hooks/usePrefersTheme";

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
export const Logo = forwardRef((props: LogoProps, ref: ForwardedRef<HTMLImageElement>) => {
  let [theme] = usePrefersTheme("light");
  const { inverse, ...delegated } = props;

  if (inverse && theme === "light") theme = "dark";
  else if (inverse && theme === "dark") theme = "light";

  return theme === "dark" ? (
    <Icon ref={ref} src={TyleLogoDarkRedIcon} {...delegated} />
  ) : (
    <Icon ref={ref} src={TyleLogoWhiteIcon} {...delegated} />
  );
});

Logo.displayName = "Logo";

/**
 * An animation wrapper for the Logo component
 *
 * @see https://github.com/framer/motion
 */
export const MotionLogo = motion(Logo);
