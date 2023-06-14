interface SvgComponentProps {
  size?: number;
  color?: string;
  props?: React.SVGProps<SVGSVGElement>;
}

const SvgComponent = ({ size = 1, color = "#000", props }: SvgComponentProps) => (
  <svg xmlns="http://www.w3.org/2000/svg" width={37} height={36} fill="none" transform={`scale(${size})`} {...props}>
    <path
      fill={color}
      d="M12.228.911a1.289 1.289 0 0 0-1.823 0L1.29 10.026a1.289 1.289 0 0 0 0 1.823l4.204 4.204a.5.5 0 0 0 .707 0l4.66-4.66a.645.645 0 0 1 .91 0l.912.912a.645.645 0 0 1 0 .911l-4.66 4.66a.5.5 0 0 0 0 .707l3.851 3.85a.5.5 0 0 0 .707 0l2.837-2.837a.645.645 0 0 1 .911 0l.912.912a.644.644 0 0 1 0 .911l-2.837 2.837a.5.5 0 0 0 0 .707l3.85 3.85a.5.5 0 0 0 .707 0l4.66-4.66a.645.645 0 0 1 .911 0l.912.912a.644.644 0 0 1 0 .912l-4.66 4.659a.5.5 0 0 0 0 .707l4.204 4.204c.503.503 1.32.503 1.823 0l9.114-9.115a1.289 1.289 0 0 0 0-1.823L12.229.911Z"
    />
  </svg>
);
export default SvgComponent;
