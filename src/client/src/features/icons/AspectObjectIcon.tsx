import { useTheme } from "styled-components";

interface AspectObjectIconProps {
  size?: number;
}

const AspectObjectIcon = ({ size }: AspectObjectIconProps) => {
  const theme = useTheme();
  return (
    <svg
      width={size ?? 24}
      height={size ?? 24}
      viewBox="0 0 84 67"
      version="1.1"
      color={theme.tyle.color.sys.tertiary.on}
      xmlns="http://www.w3.org/2000/svg"
      style={{ fillRule: "evenodd", clipRule: "evenodd", strokeLinejoin: "round", strokeMiterlimit: 2 }}
    >
      <path d="M8.579,25l0,-12.5c0,-6.899 5.601,-12.5 12.5,-12.5l41.667,0c6.899,0 12.5,5.601 12.5,12.5l-0,12.5l6.25,0c2.778,0 3.106,16.667 -0,16.667l-6.25,-0l-0,12.5c-0,6.899 -5.601,12.5 -12.5,12.5l-41.667,-0c-6.899,-0 -12.5,-5.601 -12.5,-12.5l0,-12.5l-6.25,-0c-3.105,-0 -3.105,-16.667 0,-16.667l6.25,0Zm-3.963,4.167c-0.198,1.149 -0.449,2.863 -0.449,4.166c-0,1.304 0.251,3.018 0.449,4.167l3.963,0c2.301,0 4.167,1.865 4.167,4.167l-0,12.5c-0,4.599 3.734,8.333 8.333,8.333l41.667,0c4.599,0 8.333,-3.734 8.333,-8.333l0,-12.5c0,-2.302 1.866,-4.167 4.167,-4.167l3.905,0c0.179,-1.151 0.397,-2.838 0.385,-4.126c-0.013,-1.312 -0.265,-3.028 -0.467,-4.207l-3.823,-0c-2.301,-0 -4.167,-1.866 -4.167,-4.167l0,-12.5c0,-4.599 -3.734,-8.333 -8.333,-8.333l-41.667,-0c-4.599,-0 -8.333,3.734 -8.333,8.333l-0,12.5c-0,2.301 -1.866,4.167 -4.167,4.167l-3.963,-0Z" />
    </svg>
  );
};

export default AspectObjectIcon;
