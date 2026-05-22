"""Average die function"""

import sys

from ui.panels import print_error_panel

def die(message: str):
    print_error_panel(message)
    sys.exit(1)
