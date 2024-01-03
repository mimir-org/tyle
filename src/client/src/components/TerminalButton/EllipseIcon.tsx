interface EllipseIconProps {
  color: string;
}

const EllipseIcon = ({ color }: EllipseIconProps) => (
  <svg viewBox="0 0 18 18" fill="none" xmlns="http://www.w3.org/2000/svg">
    <circle cx="8.999" cy="9.001" r="8.333" fill={color} />
  </svg>
);

export default EllipseIcon;
