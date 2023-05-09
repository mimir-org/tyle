import styled from "styled-components/macro";

interface StyledBadgeProps {
  variant?: "success" | "error" | "warning" | "info";
}

const StyledBadge = styled.span<StyledBadgeProps>`
  align-items: center;
  border-radius: 99999px;
  margin: 0 4px 0 4px;
  color: ${(props) =>
    props.variant ? props.theme.tyle.color.sys.badge[props.variant].on : props.theme.tyle.color.sys.badge.success.on};
  padding: 0 8px 0 8px;
  background-color: ${(props) =>
    props.variant
      ? props.theme.tyle.color.sys.badge[props.variant].base
      : props.theme.tyle.color.sys.badge.success.base};
  border: 1px solid
    ${(props) =>
      props.variant
        ? props.theme.tyle.color.sys.badge[props.variant].on
        : props.theme.tyle.color.sys.badge.success.base};
`;

interface BadgeProps {
  children: React.ReactNode;
  variant?: "success" | "error" | "warning" | "info";
}

export default function Badge({ children, variant }: BadgeProps) {
  return <StyledBadge variant={variant}>{children}</StyledBadge>;
}
