import { ComponentStory } from "@storybook/react";
import { theme } from "../core";
import { Flexbox } from "./Flexbox";
import { Box } from "./Box";

export default {
  title: "Layouts/Flexbox",
  component: Flexbox,
};

const Template: ComponentStory<typeof Flexbox> = (args) => (
  <Flexbox {...args}>
    {[...Array(5)].map((_, i) => {
      const color = 20 * i;
      return (
        <Box key={i} p={theme.spacing.xl} bgColor={`hsl(${color},60%,60%)`}>
          ITEM
        </Box>
      );
    })}
  </Flexbox>
);

export const Default = Template.bind({});

export const DirectionColumn = Template.bind({});
DirectionColumn.args = {
  flexDirection: "column",
};

export const WithWrap = Template.bind({});
WithWrap.args = {
  flexWrap: "wrap",
};

export const WithGap = Template.bind({});
WithGap.args = {
  gap: "16px",
};

export const WithGapAndWrap = Template.bind({});
WithGapAndWrap.args = {
  ...WithWrap.args,
  ...WithGap.args,
};
