import styled from "styled-components/macro";

interface StyledBadgeProps {
  variant?: "success" | "error" | "warning" | "info";
}

const StyledBadge = styled.span<StyledBadgeProps>`
  align-items: center;
  border-radius: 99999px;
  color: ${(props) => props.theme.tyle.color.sys.badge.success.on};
  padding: 0 8px 0 8px;
  background-color: ${(props) => props.theme.tyle.color.sys.badge.success.base};
  border: 1px solid ${(props) => props.theme.tyle.color.sys.badge.success.on};
`;

interface BadgeProps {
  children: React.ReactNode;
  variant?: StyledBadgeProps;
}

export default function Badge({ children, variant }: BadgeProps) {
  return <StyledBadge {...variant}>{children}</StyledBadge>;
}
