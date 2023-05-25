interface AspectObjectIconProps {
  size?: number;
  color?: string;
  props?: React.SVGProps<SVGSVGElement>;
}

const AspectObjectIcon = ({ size, color = "#000", props }: AspectObjectIconProps) => {
  return (
    <svg xmlns="http://www.w3.org/2000/svg" width={38} height={27} scale={`transform(${size})`} fill="none" {...props}>
      <path
        fill={color}
        fillRule="evenodd"
        d="M3.707.931c0-.514.415-.931.927-.931h28.732c.512 0 .927.417.927.931v6.517c0 .514.415.931.926.931h1.854c.512 0 .927.417.927.931v8.38a.929.929 0 0 1-.927.93H35.22a.929.929 0 0 0-.926.932v6.517a.929.929 0 0 1-.927.931H4.634a.929.929 0 0 1-.927-.931v-6.517a.929.929 0 0 0-.927-.931H.927A.929.929 0 0 1 0 17.69V9.31c0-.514.415-.93.927-.93H2.78a.929.929 0 0 0 .927-.932V.931Zm3.244 1.862a.464.464 0 0 0-.463.466v7.448a.464.464 0 0 1-.464.465h-2.78a.464.464 0 0 0-.464.466v3.724c0 .257.208.466.464.466h2.78c.256 0 .464.208.464.465v7.448c0 .258.207.466.463.466H31.05a.464.464 0 0 0 .463-.466v-7.448c0-.257.208-.465.464-.465h2.78a.464.464 0 0 0 .463-.466v-3.724a.464.464 0 0 0-.463-.466h-2.78a.464.464 0 0 1-.464-.465V3.259a.464.464 0 0 0-.463-.466H6.95Z"
        clipRule="evenodd"
      />
      <path fill={color} d="M14.83 10.165V8.379h8.34v1.786h-3.109v8.456H17.94v-8.456h-3.11Z" />
    </svg>
  );
};

export default AspectObjectIcon;
