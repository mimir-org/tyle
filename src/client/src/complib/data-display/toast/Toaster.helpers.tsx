import { CheckCircle, XCircle } from "@styled-icons/heroicons-outline";
import { CSSProperties } from "react";
import { DefaultToastOptions } from "react-hot-toast";
import { TyleTheme } from "../../core";
import { layer, translucify } from "../../mixins";

export const getCustomToasterStyles = (theme: TyleTheme): DefaultToastOptions => ({
  style: {
    display: "flex",
    padding: `${theme.spacing.xs} ${theme.spacing.small}`,
    color: theme.color.surface.on,
    boxShadow: theme.shadow.small,
    borderRadius: theme.border.radius.medium,
    background: `${layer(
      translucify(theme.color.surface.tint.base, theme.elevation.levels[1].opacity),
      translucify(theme.color.surface.base, theme.state.enabled.opacity)
    )}`,
    font: theme.typography.sys.roles.label.large.font,
    letterSpacing: theme.typography.sys.roles.label.large.letterSpacing,
    lineHeight: theme.typography.sys.roles.label.large.lineHeight,
  },
  success: {
    icon: <CheckCircle size={24} />,
  },
  error: {
    icon: <XCircle size={24} />,
  },
});

export const getCustomToastBarStyles = (): CSSProperties => ({
  animation: "revert",
});
