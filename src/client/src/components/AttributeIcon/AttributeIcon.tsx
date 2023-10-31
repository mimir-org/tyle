interface AttributeIconProps {
  size?: number;
  color?: string;
  props?: React.SVGProps<SVGSVGElement>;
}

const AttributeIcon = ({ size = 1, color = "#000", props }: AttributeIconProps) => (
  <svg xmlns="http://www.w3.org/2000/svg" width={26} height={28} fill="none" transform={`scale(${size})`} {...props}>
    <path
      fill={color}
      d="m23.934 12.012 1.58-1.52a1.552 1.552 0 0 0 0-2.256L17.443.467a1.703 1.703 0 0 0-2.343 0L2.209 12.872 17.455 4.4c.792-.44 1.805-.18 2.263.583l4.216 7.028Z"
    />
    <path
      fill={color}
      fillRule="evenodd"
      d="M24.367 18.95c.793-.44 1.064-1.414.607-2.177l-5.709-9.516c-.457-.762-1.47-1.023-2.263-.583L1.1 15.509c-.49.273-.802.767-.826 1.312l-.272 6c-.042.928.479 1.796 1.335 2.225l5.535 2.774a1.714 1.714 0 0 0 1.594-.034l15.901-8.835Zm-20.52 1.438c-.765.426-1.028 1.368-.586 2.105.443.737 1.422.99 2.188.564.765-.425 1.028-1.368.586-2.105-.442-.737-1.422-.99-2.188-.564Z"
      clipRule="evenodd"
    />
  </svg>
);
export default AttributeIcon;
