import { useTheme } from "styled-components";

interface AttributeIconProps {
  size?: number;
}

const AttributeIcon = ({ size }: AttributeIconProps) => {
  const theme = useTheme();

  return (
    <svg
      width={size ?? 24}
      height={size ?? 24}
      viewBox="0 0 61 42"
      version="1.1"
      xmlSpace="preserve"
      color={theme.tyle.color.sys.tertiary.on}
      fill={theme.tyle.color.sys.tertiary.on}
      stroke={theme.tyle.color.sys.tertiary.on}
      style={{
        fillRule: "evenodd",
        clipRule: "evenodd",
        strokeLinecap: "round",
        strokeLinejoin: "round",
        strokeMiterlimit: "1.5",
      }}
    >
      <g transform="matrix(50,0,0,50,51.0417,36.3444)"></g>
      <text
        x="26.042px"
        y="36.344px"
        style={{
          fontFamily: "ArialMT, Arial, sans-serif",
          fontSize: "50px",
        }}
      >
        x
      </text>
      <path
        d="M2.083,18.75l8.334,0l4.166,20.833l4.167,-37.5l39.583,0"
        style={{
          fill: "none",
          strokeWidth: "4.17px",
        }}
      />
    </svg>
  );
};

export default AttributeIcon;
