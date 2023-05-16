import { useTheme } from "styled-components";

interface RdsIconProps {
  size?: number;
}

const RdsIcon = ({ size }: RdsIconProps) => {
  const theme = useTheme();

  return (
    <svg
      width={size ?? 24}
      height={size ?? 24}
      viewBox="0 0 67 67"
      version="1.1"
      xmlSpace="preserve"
      color={theme.tyle.color.sys.tertiary.on}
      stroke={theme.tyle.color.sys.tertiary.on}
      fill={theme.tyle.color.sys.tertiary.on}
      style={{
        fillRule: "evenodd",
        clipRule: "evenodd",
        strokeLinejoin: "round",
        strokeMiterlimit: "2",
      }}
    >
      <path d="M66.667,3.97l-0,58.726c-0,2.192 -1.779,3.971 -3.971,3.971l-58.726,-0c-2.191,-0 -3.97,-1.779 -3.97,-3.971l-0,-58.726c-0,-2.191 1.779,-3.97 3.97,-3.97l58.726,0c2.192,0 3.971,1.779 3.971,3.97Zm-62.5,0.197l-0,58.333l58.333,-0l0,-58.333l-58.333,-0Z" />
      <g transform="matrix(37.9484,0,0,37.9484,59.6823,46.9155)"></g>
      <text
        x="9.06px"
        y="46.915px"
        style={{
          fontFamily: "ArialMT, Arial, sans-serif",
          fontSize: "37.948px",
        }}
      >
        AB
      </text>
    </svg>
  );
};

export default RdsIcon;
