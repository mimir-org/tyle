import { ComponentStory } from "@storybook/react";
import { Box } from "complib/layouts";
import { Text } from "complib/text";
import { SettingsSection } from "features/settings/common/settings-section/SettingsSection";

export default {
  title: "Settings/Common/SettingsSection",
  component: SettingsSection,
};

const Template: ComponentStory<typeof SettingsSection> = (args) => <SettingsSection {...args}></SettingsSection>;

export const Default = Template.bind({});
Default.args = {
  title: "Your section title",
  children: (
    <Box>
      <Text>Your section content A</Text>
      <Text>Your section content B</Text>
      <Text>Your section content C</Text>
      <Text>Your section content D</Text>
    </Box>
  ),
};
