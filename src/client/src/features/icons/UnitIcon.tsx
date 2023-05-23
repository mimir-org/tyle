import { useTheme } from "styled-components";

interface UnitIconProps {
  size?: number;
}

const UnitIcon = ({ size }: UnitIconProps) => {
  const theme = useTheme();

  return (
    <svg
      width={size ?? 24}
      height={size ?? 24}
      viewBox="0 0 76 76"
      version="1.1"
      xmlSpace="preserve"
      color={theme.tyle.color.sys.tertiary.on}
      stroke={theme.tyle.color.sys.tertiary.on}
      style={{
        fillRule: "evenodd",
        clipRule: "evenodd",
        strokeLinecap: "round",
        strokeLinejoin: "round",
        strokeMiterlimit: "1.5",
      }}
    >
      <path d="M19.944,0.793l55.095,55.095c1.057,1.057 1.057,2.773 -0,3.83l-15.321,15.321c-1.057,1.057 -2.773,1.057 -3.83,-0l-55.095,-55.095c-1.057,-1.057 -1.057,-2.774 -0,-3.831l15.32,-15.32c1.057,-1.057 2.774,-1.057 3.831,-0Zm-15.174,17.235l53.033,53.033l13.258,-13.258l-53.031,-53.034l-13.26,13.259Z" />
      <path
        d="M54.857,68.115l5.892,-5.892"
        style={{
          fill: "none",
          strokeWidth: "2.08px",
        }}
      />
      <path
        d="M48.964,62.223l5.893,-5.893"
        style={{
          fill: "none",
          strokeWidth: "2.08px",
        }}
      />
      <path
        d="M43.072,56.33l5.892,-5.892"
        style={{
          fill: "none",
          strokeWidth: "2.08px",
        }}
      />
      <path
        d="M37.177,50.436l5.893,-5.893"
        style={{
          fill: "none",
          strokeWidth: "2.08px",
        }}
      />
      <path
        d="M31.287,44.545l5.892,-5.893"
        style={{
          fill: "none",
          strokeWidth: "2.08px",
        }}
      />
      <path
        d="M25.394,38.652l5.893,-5.892"
        style={{
          fill: "none",
          strokeWidth: "2.08px",
        }}
      />
      <path
        d="M19.502,32.76l5.892,-5.893"
        style={{
          fill: "none",
          strokeWidth: "2.08px",
        }}
      />
      <path
        d="M13.609,26.867l5.893,-5.892"
        style={{
          fill: "none",
          strokeWidth: "2.08px",
        }}
      />
      <path
        d="M7.716,20.975l5.893,-5.893"
        style={{
          fill: "none",
          strokeWidth: "2.08px",
        }}
      />
      <path
        d="M51.911,65.169l2.946,-2.946"
        style={{
          fill: "none",
          strokeWidth: "2.08px",
        }}
      />
      <path
        d="M46.018,59.276l2.946,-2.946"
        style={{
          fill: "none",
          strokeWidth: "2.08px",
        }}
      />
      <path
        d="M40.124,53.382l2.946,-2.946"
        style={{
          fill: "none",
          strokeWidth: "2.08px",
        }}
      />
      <path
        d="M34.233,47.491l2.946,-2.946"
        style={{
          fill: "none",
          strokeWidth: "2.08px",
        }}
      />
      <path
        d="M28.34,41.599l2.947,-2.947"
        style={{
          fill: "none",
          strokeWidth: "2.08px",
        }}
      />
      <path
        d="M22.448,35.706l2.946,-2.946"
        style={{
          fill: "none",
          strokeWidth: "2.08px",
        }}
      />
      <path
        d="M16.555,29.814l2.947,-2.947"
        style={{
          fill: "none",
          strokeWidth: "2.08px",
        }}
      />
      <path
        d="M10.663,23.921l2.946,-2.946"
        style={{
          fill: "none",
          strokeWidth: "2.08px",
        }}
      />
    </svg>
  );
};

export default UnitIcon;
