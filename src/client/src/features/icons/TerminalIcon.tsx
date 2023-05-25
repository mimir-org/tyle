interface TerminalIconProps {
  size?: number;
  fillColor?: string;
  color?: string;
  props?: React.SVGProps<SVGSVGElement>;
}
const TerminalIcon = ({ size = 1, fillColor = "none", color = "#000", props }: TerminalIconProps) => {
  return (
    <svg xmlns="http://www.w3.org/2000/svg" width={35} height={35} transform={`scale(${size})`} fill="none" {...props}>
      <path
        fill={fillColor}
        fillRule="evenodd"
        d="M3.063 1.313a1.75 1.75 0 0 0-1.75 1.75v28.875c0 .966.783 1.75 1.75 1.75h28.875a1.75 1.75 0 0 0 1.75-1.75V3.063a1.75 1.75 0 0 0-1.75-1.75H3.063ZM17.5 28.576c6.117 0 11.076-4.96 11.076-11.076 0-6.117-4.96-11.076-11.076-11.076-6.117 0-11.076 4.96-11.076 11.076 0 6.117 4.96 11.076 11.076 11.076Z"
        clipRule="evenodd"
      />
      <path
        fill={color}
        fillRule="evenodd"
        d="M3.063 2.625a.438.438 0 0 0-.438.438v28.875c0 .241.196.437.438.437h28.875a.438.438 0 0 0 .437-.438V3.063a.438.438 0 0 0-.438-.437H3.063ZM0 3.063A3.063 3.063 0 0 1 3.063 0h28.875A3.062 3.062 0 0 1 35 3.063v28.875A3.062 3.062 0 0 1 31.937 35H3.063A3.062 3.062 0 0 1 0 31.937V3.063Zm17.5 4.674c-5.392 0-9.763 4.371-9.763 9.763 0 5.392 4.371 9.763 9.763 9.763 5.392 0 9.763-4.371 9.763-9.763 0-5.392-4.371-9.763-9.763-9.763ZM5.112 17.5c0-6.842 5.546-12.388 12.388-12.388S29.888 10.658 29.888 17.5 24.342 29.888 17.5 29.888 5.112 24.342 5.112 17.5Z"
        clipRule="evenodd"
      />
    </svg>
  );
};

export default TerminalIcon;
